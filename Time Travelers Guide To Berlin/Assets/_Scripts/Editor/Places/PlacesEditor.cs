//using UnityEditor;
//using UnityEngine;
//using System.Collections;

//[CustomEditor(typeof(PlacesController))]
//public class PlacesEditor : Editor {

//    private SerializedProperty placesIconProperty;
//    private SerializedProperty placesNameProperty;
//    private SerializedProperty placesCoordinateProperty;
//    private bool[] showPlaces = new bool[PlacesController.numPlacesToVisit];

//    private const string placesCotrollerPropIconName = "placesIcons";
//    private const string placesControllerPropPlacesName = "placesNames";
//    private const string placesControllerPropCoordinatesName = "placesCoordinates";

//    int numberOfPlaces;

//    private void OnEnable()
//    {
//        placesIconProperty = serializedObject.FindProperty(placesCotrollerPropIconName);
//        placesNameProperty = serializedObject.FindProperty(placesControllerPropPlacesName);
//        placesCoordinateProperty = serializedObject.FindProperty(placesControllerPropCoordinatesName);

//    }

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();

//        placesIconProperty.arraySize = PlacesController.numPlacesToVisit;
//        placesNameProperty.arraySize = PlacesController.numPlacesToVisit;
//        placesCoordinateProperty.arraySize = PlacesController.numPlacesToVisit;

//        for (int i = 0; i < PlacesController.numPlacesToVisit; i++)
//        {
//            ShowPlacesGUI(i);
//        }

//        serializedObject.ApplyModifiedProperties();
//    }

//    private void ShowPlacesGUI(int index)
//    {
//        EditorGUILayout.BeginVertical(GUI.skin.box);
//        EditorGUI.indentLevel++;

//        numberOfPlaces = EditorGUILayout.IntField("Number of places", 1);

//        if (showPlaces[index])
//        {
//            EditorGUILayout.PropertyField(placesNameProperty.GetArrayElementAtIndex(index), new GUIContent("Place Name"));
//            EditorGUILayout.PropertyField(placesIconProperty.GetArrayElementAtIndex(index),new GUIContent("Place Icon"));
//            EditorGUILayout.PropertyField(placesCoordinateProperty.GetArrayElementAtIndex(index),new GUIContent("Place Coordinates"));
//        }



//        EditorGUI.indentLevel--;
//        EditorGUILayout.EndVertical();


//    }
//}
