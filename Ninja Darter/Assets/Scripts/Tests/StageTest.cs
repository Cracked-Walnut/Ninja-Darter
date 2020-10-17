using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {

    public class StageTest {
        
        private GameObject _nodePrefab;

        void Awake() {
            _nodePrefab = GameObject.Find("SpawnNode");
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void NodeIsSpawningRoom_Test() {
            // Assert.AreEqual(1, 0);
        }
    }
}
