using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PFX
{
    public static class Tools
    {
        /// <summary>
        /// //Smoothly rotates any gameobject.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="targetRotation"></param>
        /// <param name="spd"></param>
        public static void SmoothRotate(Transform t, Vector3 targetRotation, float spd)
        {
            t.rotation = Quaternion.Lerp(t.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * spd);
        }

        /// <summary>
        /// //Updates the fill amount of a UI element. Used for slider functuanality with custom shapes. Make sure sprite mode is set to "Filled" in editor.
        /// </summary>
        /// <param name="fill"></param>
        /// <param name="add"></param>
        /// <param name="currentAmount"></param>
        /// <param name="updateAmount"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static float UpdateUIFill(Image fill, bool add, float currentAmount, int updateAmount, int maxValue)
        {
            if (add)
            {
                currentAmount += updateAmount;

                if (currentAmount > maxValue)
                    currentAmount = maxValue;
            }
            else
            {
                currentAmount -= updateAmount;

                if (currentAmount < 0)
                    currentAmount = 0;
            }

            fill.fillAmount = (float)currentAmount / maxValue;
            return currentAmount;
        }

        /// <summary>
        /// A timer that counts up and returns true once it equals the given max time.
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="maxTime"></param>
        /// <param name="updatedTime"></param>
        /// <returns></returns>
        public static bool Timer(float timer, float maxTime, out float updatedTime)
        {
            timer += Time.deltaTime;
            updatedTime = timer;
            if (timer >= maxTime)
            {
                timer -= maxTime;
                updatedTime = timer;
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// //Gets a direction based on the given angle.
        /// </summary>
        /// <param name="eularY"></param>
        /// <param name="angleToDegrees"></param>
        /// <returns></returns>
        public static Vector3 DirectionFromAngle(float eularY, float angleToDegrees)
        {
            angleToDegrees += eularY;

            return new Vector3(Mathf.Sin(angleToDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleToDegrees * Mathf.Rad2Deg));
        }

        /// <summary>
        /// //Loads scene via the given index.
        /// </summary>
        /// <param name="sceneIndex"></param>
        public static void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        /// <summary>
        /// //Loads scene via the scene's name.
        /// </summary>
        /// <param name="sceneName"></param>
        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// //Closes the game.
        /// </summary>
        public static void Quit()
        {
            Application.Quit();
        }
    }
}
