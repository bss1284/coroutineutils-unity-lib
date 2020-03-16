using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BSS.CoroutineUtils {
    public static class CoroutineHelper 
    {
        private static InnerCoroutineMonoBehaviour _innerInstance;
        private static InnerCoroutineMonoBehaviour innerInstance {
            get {
                if (_innerInstance==null) {
                    var obj = new GameObject("Coroutine Helper");
                    UnityEngine.Object.DontDestroyOnLoad(obj);
                    _innerInstance = obj.AddComponent<InnerCoroutineMonoBehaviour>();
                }
                return _innerInstance;
            }
        }
    
    

        public static Coroutine Invoke(IEnumerator coroutine) {
            return innerInstance.StartCoroutine(coroutine);
        }
        public static Coroutine InvokeUpdate(bool cancelOnSceneLoad,int frame,Action act) {
            if(frame < 1)
                throw new Exception("The frame must be at least greater than 1.");
            return innerInstance.StartCoroutine(innerInstance.InvokeUpdateAsync(cancelOnSceneLoad,frame,act));
        }
        public static Coroutine InvokeUpdate(bool cancelOnSceneLoad, Action act) {
            return innerInstance.StartCoroutine(innerInstance.InvokeUpdateAsync(cancelOnSceneLoad, 1, act));
        }

        public static Coroutine InvokeAfterSeconds(bool cancelOnSceneLoad,float sec,Action act) {
            return innerInstance.StartCoroutine(innerInstance.InvokeAfterSecodnsAsync(cancelOnSceneLoad,sec,act));
        }
        public static Coroutine InvokeAfterCondition(bool cancelOnSceneLoad, Func<bool> condition, Action act) {
            return innerInstance.StartCoroutine(innerInstance.InvokeAfterConditionAsync(cancelOnSceneLoad, condition, act));
        }

        public static void StopAfterCondition(this Coroutine target,Func<bool> condition) {
            innerInstance.StartCoroutine(innerInstance.StopAfterConditionAsync(target, condition));
        }
        public static void StopAfterCondition(this Coroutine target, Func<bool> condition,Action endAct) {
            innerInstance.StartCoroutine(innerInstance.StopAfterConditionAsync(target, condition, endAct));
        }

        public static void StopAfterSeconds(this Coroutine target, float sec) {
            innerInstance.StartCoroutine(innerInstance.StopAfterSecondsAsync(target, sec));
        }
        public static void StopAfterSeconds(this Coroutine target, float sec, Action endAct) {
            innerInstance.StartCoroutine(innerInstance.StopAfterSecondsAsync(target, sec, endAct));
        }
    }
}
