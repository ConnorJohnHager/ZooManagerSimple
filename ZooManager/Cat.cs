﻿using System;

namespace ZooManager
{
    public class Cat : Animal
    {
        public Cat(string name)
        {
            emoji = "🐱";
            species = "cat";
            this.name = name;
            reactionTime = new Random().Next(1, 6); // reaction time 1 (fast) to 5 (medium)Cat
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a cat. Meow.");
            TaskProcess();
        }

        /* Note that our cat is currently not very clever about its hunting.
         * It will always try to attack "up" and will only seek "down" if there
         * is no mouse above it. This does not affect the cat's effectiveness
         * very much, since the overall logic here is "look around for a mouse and
         * attack the first one you see." This logic might be less sound once the
         * cat also has a predator to avoid, since the cat may not want to run in
         * to a square that sets it up to be attacked!
         */
        public void Hunt()
        {
            if (Game.Seek(location.x, location.y, Direction.up, "mouse") || Game.Seek(location.x, location.y, Direction.up, "chick"))
            {
                if (Game.Attack(this, Direction.up)) return;
            }
            else if (Game.Seek(location.x, location.y, Direction.down, "mouse") || Game.Seek(location.x, location.y, Direction.down, "chick"))
            {
                if (Game.Attack(this, Direction.down)) return;
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "mouse") || Game.Seek(location.x, location.y, Direction.left, "chick"))
            {
                if (Game.Attack(this, Direction.left)) return;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "mouse") || Game.Seek(location.x, location.y, Direction.right, "chick"))
            {
                if (Game.Attack(this, Direction.right)) return;
            }
        }

        /* Cats run from raptors! */
        public void Flee()
        {
            if (Game.Seek(location.x, location.y, Direction.up, "raptor"))
            {
                if (Game.Retreat(this, Direction.down)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.down, "raptor"))
            {
                if (Game.Retreat(this, Direction.up)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.left, "raptor"))
            {
                if (Game.Retreat(this, Direction.right)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.right, "raptor"))
            {
                if (Game.Retreat(this, Direction.left)) return;
            }
        }

        public void TaskProcess() // Priority is to flee over hunt
        {
            if (Game.Seek(location.x, location.y, Direction.up, "raptor"))
            {
                if (Game.Retreat(this, Direction.down)) return;
            }
            else if (Game.Seek(location.x, location.y, Direction.down, "raptor"))
            {
                if (Game.Retreat(this, Direction.up)) return;
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "raptor"))
            {
                if (Game.Retreat(this, Direction.right)) return;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "raptor"))
            {
                if (Game.Retreat(this, Direction.left)) return;
            }
            else if (Game.Seek(location.x, location.y, Direction.up, "mouse") || Game.Seek(location.x, location.y, Direction.up, "chick"))
            {
                if (Game.Attack(this, Direction.up)) return;
            }
            else if (Game.Seek(location.x, location.y, Direction.down, "mouse") || Game.Seek(location.x, location.y, Direction.down, "chick"))
            {
                if (Game.Attack(this, Direction.down)) return;
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "mouse") || Game.Seek(location.x, location.y, Direction.left, "chick"))
            {
                if (Game.Attack(this, Direction.left)) return;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "mouse") || Game.Seek(location.x, location.y, Direction.right, "chick"))
            {
                if (Game.Attack(this, Direction.right)) return;
            }
        }
    }
}

