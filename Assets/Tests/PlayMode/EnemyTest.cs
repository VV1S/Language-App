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
        var gameObject = new GameObject();
        //var player = gameObject.AddComponent<Player>();

        // Act
        //player.StartFire();
        yield return new WaitForSeconds(0.1f);
        var projectile = GameObject.Find("laser");

        // Assert
        Assert.NotNull(projectile);
        //Assert.AreNotEqual(projectile.transform.position, player.transform.position);
    }
}
