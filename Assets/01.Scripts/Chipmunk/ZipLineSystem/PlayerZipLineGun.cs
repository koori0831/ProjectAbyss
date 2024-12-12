using System;
using UnityEngine;

namespace Chipmunk.ZipLineSystem
{
    public class PlayerZipLineGun : ZipLineShooter, IPlayerComponent
    {
        Player player;
        public void Initialize(Player player)
        {
            this.player = player;
            player.InputCompo.ZipShootEvent += ShootZipline;
        }
        void OnDestroy()
        {
            player.InputCompo.ZipShootEvent -= ShootZipline;
        }

        private void ShootZipline()
        {
            Vector2 dir = player.InputCompo.MousePos - (Vector2)transform.position;
            Shoot(dir);
        }
    }
}
