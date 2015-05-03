using UnityEngine;
using System.Collections;

namespace Utils
{
    public class IterateChildrenUtil
    {
        public delegate bool ChildHandler(GameObject child);
        // Use this for initialization
        public static void IterateChildren(GameObject gameObject, ChildHandler childHandler, bool recursive)
        {
            DoIterate(gameObject, childHandler, recursive);
        }

        private static bool DoIterate(GameObject gameObject, ChildHandler childHandler, bool recursive)
        {
            foreach (Transform child in gameObject.transform)
            {
                childHandler(child.gameObject);

                if (recursive)
				{
					if(DoIterate(child.gameObject, childHandler, true) == false)
					{
						break;
					}
				}
                    
            }

			return true;
        }
    }
}