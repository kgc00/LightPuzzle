using System;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Tools {
    public static class PropertyDrawerUtility
    {
        public static T GetActualObjectForSerializedProperty<T>(FieldInfo fieldInfo, SerializedProperty property) {
            var obj = fieldInfo.GetValue(property.serializedObject.targetObject);
            if (obj == null) { return default; }
 
            T actualObject;
            if (obj.GetType().IsArray)
            {
                var index = Convert.ToInt32(new string(property.propertyPath.Where(c => char.IsDigit(c)).ToArray()));
                actualObject = ((T[])obj)[index];
            }
            else
            {
                actualObject = obj is T t ? t : default;
            }
            return actualObject;
        }
    }
}