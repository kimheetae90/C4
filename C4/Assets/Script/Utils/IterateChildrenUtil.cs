using UnityEngine;
using System.Collections;

namespace Utils
{
    public class IterateChildrenUtil
    {
        public delegate void ChildHandler(GameObject child);
        // Use this for initialization
        public static void IterateChildren(GameObject gameObject, ChildHandler childHandler, bool recursive)
        {
            DoIterate(gameObject, childHandler, recursive);
        }

        private static void DoIterate(GameObject gameObject, ChildHandler childHandler, bool recursive)
        {
            foreach (Transform child in gameObject.transform)
            {
                childHandler(child.gameObject);
                if (recursive)
                    DoIterate(child.gameObject, childHandler, true);
            }
        }
    }
}