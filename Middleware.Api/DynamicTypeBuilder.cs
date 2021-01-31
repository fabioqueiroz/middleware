using Middleware.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public static class DynamicTypeBuilder
    {
        public static object CreateNewObject(Dictionary<string, object> valuesDictionary)
        {
            var myType = CompileResultType(valuesDictionary);
            return Activator.CreateInstance(myType);
        }

        public static Type CompileResultType(Dictionary<string, object> valuesDictionary)
        {
            TypeBuilder tb = GetTypeBuilder();
            Type objType = Type.GetType("System.Object");
            ConstructorInfo objCtor = objType.GetConstructor(new Type[0]);
            //ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            //ConstructorBuilder constructor = tb.DefineConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Any, new Type[0]);
            ConstructorBuilder constructor = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);

            ILGenerator ctorIL = constructor.GetILGenerator();
            ctorIL.Emit(OpCodes.Ldarg_0);
            ctorIL.Emit(OpCodes.Call, objCtor);
            ctorIL.Emit(OpCodes.Ret);

            // The dictionary contains the property name (property.Key, string) and its value, from which we obatin the Type                
            foreach (var property in valuesDictionary)
            {
                if (property.Value != null)
                {
                    CreateProperty(tb, property.Key, property.Value.GetType()); 
                }
            }
            Type objectType = tb.CreateType();
            return objectType;
        }

        private static TypeBuilder GetTypeBuilder()
        {
            var typeSignature = "EntityDynamicType";
            //AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(typeSignature), AssemblyBuilderAccess.Run);
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(typeof(DeviceModel).FullName), AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("EntityModule");
            //TypeBuilder typeBuilder = moduleBuilder.DefineType(typeSignature,
            //        TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout,
            //        null);
            TypeBuilder typeBuilder = moduleBuilder.DefineType(typeof(DeviceModel).Name,
                    TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.AutoLayout,
                    null);

            return typeBuilder;
        }

        private static void CreateProperty(TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            FieldBuilder fieldBuilder = typeBuilder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);

            MethodBuilder getPropertyMethodBuilder = typeBuilder.DefineMethod("get_" + propertyName, 
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);

            ILGenerator getIl = getPropertyMethodBuilder.GetILGenerator();
            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropertyMethodBuilder = typeBuilder.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |MethodAttributes.SpecialName |MethodAttributes.HideBySig, null, new[] { propertyType });

            ILGenerator setIl = setPropertyMethodBuilder.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);
            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropertyMethodBuilder);
            propertyBuilder.SetSetMethod(setPropertyMethodBuilder);
        }
    }
}
