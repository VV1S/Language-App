using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyTest
{
    [UnityTest]
    public IEnumerator FireTest()
    {
        // Arrange
        var playerObject = new GameObject();
        var player = playerObject.AddComponent<Player>();
        player.laserPrefab = Resources.Load<GameObject>("Prefabs/Player Laser");
        player.shootSound = Resources.Load<AudioClip>("Audio/laserSmall_004");



        // Act
        player.StartFire();
        var projectile = GameObject.FindGameObjectWithTag("Projectile");
        yield return new WaitForSeconds(0.01f);

        // Assert
        Assert.AreNotEqual(projectile.transform.position, player.transform.position);
    }
}
