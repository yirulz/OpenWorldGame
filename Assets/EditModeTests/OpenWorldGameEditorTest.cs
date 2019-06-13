using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class OpenWorldGameEditorTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void OpenWorldGameEditorTestSimplePasses()
        {
            // Use the Assert class to test conditions


        }

        [Test]
        public void Find_Player()
        {
            var player = GameObject.FindObjectOfType<Player>();
            Assert.IsTrue(player != null);

            Debug.Log("Player Found");

            LogAssert.Expect(LogType.Log, "Player Found");
        }

        public void FindCollider()
        {
            var player = GameObject.FindObjectOfType<Player>();
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator Check_Player_Damaged()
        {
            //Detecting if player exists
            var player = GameObject.FindObjectOfType<Player>();
            //Set oldHealth to players health
            var oldHealth = player.health;
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
            //If players health is not same as oldHealth
            Assert.IsTrue(player.health != oldHealth);
            

        }
    }
}
