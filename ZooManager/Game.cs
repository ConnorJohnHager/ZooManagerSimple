﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZooManager
{
    public static class Game
    {
        static public int numCellsX = 4;
        static public int numCellsY = 4;

        static private int maxCellsX = 10;
        static private int maxCellsY = 10;


        static public List<List<Zone>> animalZones = new List<List<Zone>>();
        static public Zone holdingPen = new Zone(-1, -1, null);

        static public void SetUpGame()
        {
            for (var y = 0; y < numCellsY; y++)
            {
                List<Zone> rowList = new List<Zone>();
                // Note one-line variation of for loop below!
                for (var x = 0; x < numCellsX; x++) rowList.Add(new Zone(x, y, null));
                animalZones.Add(rowList);
            }
        }

        static public void AddZones(Direction d)
        {
            if (d == Direction.down || d == Direction.up)
            {
                if (numCellsY >= maxCellsY) return; // hit maximum height!
                List<Zone> rowList = new List<Zone>();
                for (var x = 0; x < numCellsX; x++)
                {
                    rowList.Add(new Zone(x, numCellsY, null));
                }
                numCellsY++;
                if (d == Direction.down) animalZones.Add(rowList);
                // if (d == Direction.up) animalZones.Insert(0, rowList);
            }
            else // must be left or right...
            {
                if (numCellsX >= maxCellsX) return; // hit maximum width!
                for (var y = 0; y < numCellsY; y++)
                {
                    var rowList = animalZones[y];
                    // if (d == Direction.left) rowList.Insert(0, new Zone(null));
                    if (d == Direction.right) rowList.Add(new Zone(numCellsX, y, null));
                }
                numCellsX++;
            }
        }

        static public void ZoneClick(Zone clickedZone)
        {
            Console.Write("Got animal ");
            Console.WriteLine(clickedZone.emoji == "" ? "none" : clickedZone.emoji);
            Console.Write("Held animal is ");
            Console.WriteLine(holdingPen.emoji == "" ? "none" : holdingPen.emoji);
            if (clickedZone.occupant != null) clickedZone.occupant.ReportLocation();
            if (holdingPen.occupant == null && clickedZone.occupant != null)
            {
                // take animal from zone to holding pen
                Console.WriteLine("Taking " + clickedZone.emoji);
                holdingPen.occupant = clickedZone.occupant;
                holdingPen.occupant.location.x = -1;
                holdingPen.occupant.location.y = -1;
                clickedZone.occupant = null;
                ActivateAnimals();
            }
            else if (holdingPen.occupant != null && clickedZone.occupant == null)
            {
                // put animal in zone from holding pen
                Console.WriteLine("Placing " + holdingPen.emoji);
                clickedZone.occupant = holdingPen.occupant;
                clickedZone.occupant.location = clickedZone.location;
                holdingPen.occupant = null;
                Console.WriteLine("Empty spot now holds: " + clickedZone.emoji);
                ActivateAnimals();
            }
            else if (holdingPen.occupant != null && clickedZone.occupant != null)
            {
                Console.WriteLine("Could not place animal.");
                // Don't activate animals since user didn't get to do anything
            }
        }

        static public void AddAnimalToHolding(string animalType)
        {
            if (holdingPen.occupant != null) return;
            if (animalType == "cat") holdingPen.occupant = new Cat("Fluffy");
            if (animalType == "mouse") holdingPen.occupant = new Mouse("Squeaky");
            if (animalType == "raptor") holdingPen.occupant = new Raptor("Chance the Raptor");
            if (animalType == "chick") holdingPen.occupant = new Chick("Tweety (uncopyrighted)");
            Console.WriteLine($"Holding pen occupant at {holdingPen.occupant.location.x},{holdingPen.occupant.location.y}");
            ActivateAnimals();
        }

        static public void ActivateAnimals()
        {
            for (var r = 1; r < 11; r++) // reaction times from 1 to 10
            {
                for (var y = 0; y < numCellsY; y++)
                {
                    for (var x = 0; x < numCellsX; x++)
                    {
                        var zone = animalZones[y][x];
                        if (zone.occupant != null && zone.occupant.reactionTime == r && zone.occupant.TurnCheck == false)
                        {
                            zone.occupant.Activate();
                        }
                    }
                }
            }
            for (var y = 0; y < numCellsY; y++)
            {
                for (var x = 0; x < numCellsX; x++)
                {
                    var zone = animalZones[y][x];
                    if (zone.occupant != null)
                    {
                        zone.occupant.TurnCheck = false;
                    }
                }
            }
        }

        static public bool Seek(int x, int y, Direction d, string target, int distance)
        {
            if (target == "null") // Searching for an empty spot
            {
                switch (d)
                {
                    case Direction.up:
                        y = y - distance;
                        break;
                    case Direction.down:
                        y = y + distance;
                        break;
                    case Direction.left:
                        x = x - distance;
                        break;
                    case Direction.right:
                        x = x + distance;
                        break;
                }
                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
                if (animalZones[y][x].occupant == null) return true;
            }
            else
            {
                switch (d)
                {
                    case Direction.up:
                        y = y - distance;
                        break;
                    case Direction.down:
                        y = y + distance;
                        break;
                    case Direction.left:
                        x = x - distance;
                        break;
                    case Direction.right:
                        x = x + distance;
                        break;
                }
                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
                if (animalZones[y][x].occupant == null) return false;
                if (animalZones[y][x].occupant.species == target)
                {
                    return true;
                }
            }
            return false;
        }

        static public int Move(Animal animal, Direction d, int distance)
        {
            int movedDistance = 0;
            int x = animal.location.x;
            int y = animal.location.y;

            for (int i = 0; i < distance; i++)
            {
                switch (d)
                {
                    case Direction.up:
                        y--;
                        break;
                    case Direction.down:
                        y++;
                        break;
                    case Direction.left:
                        x--;
                        break;
                    case Direction.right:
                        x++;
                        break;
                }
                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) break;
                if (animalZones[y][x].occupant == null)
                {
                    animalZones[animal.location.y][animal.location.x].occupant = null;
                    animalZones[y][x].occupant = animal;
                    movedDistance++;
                }
                else
                {
                    break;
                }
            }
            return movedDistance;
        }


        static public bool Attack(Animal attacker, Direction d)
        {
            Console.WriteLine($"{attacker.name} is attacking {d.ToString()}");
            int x = attacker.location.x;
            int y = attacker.location.y;

            switch (d)
            {
                case Direction.up:
                    if (animalZones[y - 1][x].occupant != null)
                    {
                        animalZones[y - 1][x].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
                case Direction.down:
                    if (animalZones[y + 1][x].occupant != null)
                    {
                        animalZones[y + 1][x].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
                case Direction.left:
                    if (animalZones[y][x - 1].occupant != null)
                    {
                        animalZones[y][x - 1].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
                case Direction.right:
                    if (animalZones[y][x + 1].occupant != null)
                    {
                        animalZones[y][x + 1].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
            }
            return false; // nothing to hunt
        }

        static public bool Retreat(Animal runner, Direction d, int distance)
        {
            Console.WriteLine($"{runner.name} is retreating {d.ToString()}");
            int x = runner.location.x;
            int y = runner.location.y;

            switch (d)
            {
                case Direction.up:
                    if (y > 0 && animalZones[y - distance][x].occupant == null)
                    {
                        animalZones[y - distance][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false; // retreat was not successful
                case Direction.down:
                    if (y < numCellsY && animalZones[y + distance][x].occupant == null)
                    {
                        animalZones[y + distance][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false;
                case Direction.left:
                    if (x > 0 && animalZones[y][x - distance].occupant == null)
                    {
                        animalZones[y][x - distance].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false;
                case Direction.right:
                    if (x < numCellsX && animalZones[y][x + distance].occupant == null)
                    {
                        animalZones[y][x + distance].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false;
            }
            return false; // cannot retreat
        }
    }
}

