using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WarChess
{
    /// <summary>
    /// Manages fight related entities (enemies, heroes, maps, grids, etc.)
    /// </summary>
    public class FightWorldManager
    {
        public EFightState state = EFightState.Idle;

        /// <summary>
        /// Current turn's unit
        /// </summary>
        protected FightUnitBase current;
        
        public FightUnitBase Current
        {
            get { return current; }
        }

        public List<Hero> heroes;
        
        public List<Enemy> enemies;

        public int roundCount;
        
        public FightWorldManager()
        {
            heroes = new List<Hero>();
            enemies = new List<Enemy>();
            ChangeState(EFightState.Idle);
        }
        
        public void OnUpdate(float dt)
        {
            if (current != null && current.OnUpdate(dt))
            {
                // TODO
            }
            else
            {
                current = null;
            }
        }
        
        /// <summary>
        /// Switch fight state
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(EFightState newState)
        {
            FightUnitBase currentCache = current;
            state = newState;
            
            switch (state)
            {
                case EFightState.Idle:
                    currentCache = new FightIdle();
                    break;
                case EFightState.Enter:
                    currentCache = new FightEnter();
                    break;
                case EFightState.Player:
                    currentCache = new FightPlayerUnit();
                    break;
            }
            
            currentCache.Init();
        }

        // initialize some information, such as the turns, the heroes, the enemies, etc.
        public void EnterFight()
        {
            roundCount = 1;
            heroes = new List<Hero>();
            enemies = new List<Enemy>();
            
            // saves the enemies in the scene
            enemies = Object.FindObjectsOfType<Enemy>().ToList();

            foreach (var enemy in enemies)
            {
                // change the block type of the enemy's position to obstacle
                GameApp.MapManager.ChangeBlockType(enemy.rowIndex, enemy.colIndex, EBlockType.Obstacle);
            }
        }
        
        public void AddHero(Block b, Dictionary<string, string> data)
        {
            GameObject go = Object.Instantiate(Resources.Load($"Model/{data["Model"]}")) as GameObject;
            if (go != null)
            {
                go.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, -1);
                Hero hero = go.AddComponent<Hero>();
                hero.Init(data, b.rowIndex, b.colIndex);
                b.type = EBlockType.Obstacle;
                heroes.Add(hero);
            }
        }
    }
}