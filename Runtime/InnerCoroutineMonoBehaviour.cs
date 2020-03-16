using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace BSS.CoroutineUtils {
    internal class InnerCoroutineMonoBehaviour : MonoBehaviour {
        internal IEnumerator InvokeUpdateAsync(bool cancelOnSceneLoad, int frame, Action act) {
            Scene curScene = SceneManager.GetActiveScene();

            while(true) {
                bool isSceneChanged = cancelOnSceneLoad && curScene != SceneManager.GetActiveScene();
                if(isSceneChanged || act == null) {
                    yield break;
                }


                act?.Invoke();
                for(int i = 0; i < frame; i++) {
                    yield return null;
                }
            }
        }

        internal IEnumerator InvokeAfterSecodnsAsync(bool cancelOnSceneLoad, float sec, Action act) {
            Scene curScene = SceneManager.GetActiveScene();

            yield return new WaitForSeconds(sec);
            if(cancelOnSceneLoad && curScene != SceneManager.GetActiveScene()) {
                yield break;
            }
            act?.Invoke();
        }

        internal IEnumerator InvokeAfterConditionAsync(bool cancelOnSceneLoad, Func<bool> condition, Action act) {
            Scene curScene = SceneManager.GetActiveScene();

            yield return new WaitUntil(() => condition == null || condition?.Invoke() == true);
            if(cancelOnSceneLoad && curScene != SceneManager.GetActiveScene()) {
                yield break;
            }
            act?.Invoke();
        }

        internal IEnumerator StopAfterConditionAsync(Coroutine target, Func<bool> condition) {
            yield return new WaitUntil(() => target == null || condition == null || condition?.Invoke() == true);
            StopCoroutine(target);
        }
        internal IEnumerator StopAfterConditionAsync(Coroutine target, Func<bool> condition,Action endAct) {
            yield return new WaitUntil(() => target == null || condition == null || condition?.Invoke() == true);
            endAct?.Invoke();
            StopCoroutine(target);
        }

        internal IEnumerator StopAfterSecondsAsync(Coroutine target, float sec) {
            yield return new WaitForSeconds(sec);
            StopCoroutine(target);
        }
        internal IEnumerator StopAfterSecondsAsync(Coroutine target, float sec,Action endAct) {
            yield return new WaitForSeconds(sec);
            endAct?.Invoke();
            StopCoroutine(target);
        }
    };
}