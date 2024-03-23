using System;
namespace ZooManager
{
	public interface IPrey
	{
        public bool Flee(Animal prey, int x, int y, string predator, int distance)
        {
            if (Game.Seek(x, y, Direction.up, predator))
            {
                if (Game.Seek(x, y, Direction.up, "null")) // check all directions for fleeing
                {
                    if(Game.Move(prey, Direction.up, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null"))
                {
                    if (Game.Move(prey, Direction.down, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null"))
                {
                    if (Game.Move(prey, Direction.left, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null"))
                {
                    if (Game.Move(prey, Direction.right, distance) > 0) return true;
                }
                return false; // can't run
            }
            if (Game.Seek(x, y, Direction.down, predator))
            {
                if (Game.Seek(x, y, Direction.up, "null")) // check all directions for fleeing
                {
                    if (Game.Move(prey, Direction.up, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null"))
                {
                    if (Game.Move(prey, Direction.down, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null"))
                {
                    if (Game.Move(prey, Direction.left, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null"))
                {
                    if (Game.Move(prey, Direction.right, distance) > 0) return true;
                }
                return false; // can't run
            }
            if (Game.Seek(x, y, Direction.left, predator))
            {
                if (Game.Seek(x, y, Direction.up, "null")) // check all directions for fleeing
                {
                    if (Game.Move(prey, Direction.up, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null"))
                {
                    if (Game.Move(prey, Direction.down, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null"))
                {
                    if (Game.Move(prey, Direction.left, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null"))
                {
                    if (Game.Move(prey, Direction.right, distance) > 0) return true;
                }
                return false; // can't run
            }
            if (Game.Seek(x, y, Direction.right, predator))
            {
                if (Game.Seek(x, y, Direction.up, "null")) // check all directions for fleeing
                {
                    if (Game.Move(prey, Direction.up, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null"))
                {
                    if (Game.Move(prey, Direction.down, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null"))
                {
                    if (Game.Move(prey, Direction.left, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null"))
                {
                    if (Game.Move(prey, Direction.right, distance) > 0) return true;
                }
                return false; // can't run
            }
            return false; // nothing to flee
        }
    }
}

