using System;
using System.Collections.Generic;

namespace func_rocket { // ������������ ��� � ��������� ������� �������� � �����
    public class LevelsTask { // �����, ������� ���������� ������� ������

        static readonly Physics standardPhysics = new(); // ����������� ��������� ������ Physics � ����������� �� ���������

        public static Level CreateLevelZero() { // ������ ������ Level, ����������� ������� ������� Zero
            return new Level("Zero",
              new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI), // ������ Rocket � ���������� ������������, ��������� ��������� � ����� ��������
              new Vector(600, 200), // ��������� ���� �� ������� ����
              (size, v) => Vector.Zero, standardPhysics); // ����������� �� ������ �� ������������
        }
        public static Level CreateLevelHeavy() { // ������ ������ Level, ����������� ������� ������� Heavy
            return new Level("Heavy",
              new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI), // ������ Rocket � ���������� ������������, ��������� ��������� � ����� ��������
              new Vector(600, 200), // ��������� ���� �� ������� ����
              (size, v) => new Vector(0, 0.9), standardPhysics); // ��������� ����������
        }

        public static Level CreateLevelUp() { // ������ ������ Level, ����������� ������� ������� Up
            return new Level("Up",
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI), // ������ Rocket � ���������� ������������, ��������� ��������� � ����� ��������
                new Vector(700, 500), // ��������� ���� �� ������� ����
                (size, v) => { // ��������� ������ �������� ���� � ������� ��������� ������
                    var d = size.Y - v.Y; // ���������� ����� ������� ����� ���������� ���� � ������ �������� �������� ����
                    return new Vector(0, -300.0 / (d + 300.0)); // ������ �������, ��������� �������� ���� 
                },
                standardPhysics);
        }

        public static Level CreateLevelWhiteHole() { // ������ ������ Level, ����������� ������� ������� CreateLevelWhiteHole
            return new Level("WhiteHole",
              new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI), // ������ Rocket � ���������� ������������, ��������� ��������� � ����� ��������
              new Vector(600, 400), // ��������� ���� �� ������� ����
              (size, v) => { // ��������� ������ �������� ���� � ������� ��������� ������
                  Vector gravityDirection = (new Vector(600, 400) - v).Normalize(); // ������ ����������� �� ����� ����������
                  double distance = (new Vector(600, 400) - v).Length; // ���������� ����� ������� ���������� � ������� ���������� ������
                  double gravity = 140 * distance / (distance * distance + 1); // ���������� �������������� ����
                  return gravity * gravityDirection; // ������, �������� �������������� ���������
              },
              standardPhysics);
        }

        public static Level CreateLevelBlackHole() { // ������ ������ Level, ����������� ������� ������� BlackHole
            return new Level("BlackHole",
              new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI), // ������ Rocket � ���������� ������������, ��������� ��������� � ����� ��������
              new Vector(700, 500), // ��������� ���� �� ������� ����
              (size, v) => { // ��������� ������ �������� ���� � ������� ��������� ������
                  Vector anomalyPos = (new Vector(200, 500) + new Vector(700, 500)) / 2; // ���������� ������ ��������
                  Vector gravityDirection = (anomalyPos - v).Normalize(); // ����������� �� ����� ����������
                  double distance = (anomalyPos - v).Length; // ���������� �� ������ ����������
                  double gravity = 300 * distance / (distance * distance + 1); // ���������� �������������� ����
                  return gravity * gravityDirection; // ���������� ������� ��������������� ���������
              },
              standardPhysics);
        }

        public static Level CreateLevelBlackAndWhite() { // ������ ������ Level, ����������� ������� ������� BlackAndWhite
            return new Level("BlackAndWhite",
              new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI), // ������ Rocket � ���������� ������������, ��������� ��������� � ����� ��������
              new Vector(600, 400), // ��������� ���� �� ������� ����
              (size, v) => { // ��������� ������ �������� ���� � ������� ��������� ������
                  Vector gravityWhite = (new Vector(600, 400) - v).Normalize(); // ������ ���������� ��� ����� ��������
                  Vector gravityBlack = ((new Vector(200, 500) + new Vector(700, 500)) / 2 - v).Normalize(); // ������ ���������� ��� ������ ��������
                  double distanceWhite = (new Vector(600, 400) - v).Length; // ���������� �� ����� ��������
                  double distanceBlack = ((new Vector(200, 500) + new Vector(700, 500)) / 2 - v).Length; // ���������� �� ������ ��������
                  double gravity = (140 * distanceWhite / (distanceWhite * distanceWhite + 1) + 300 * distanceBlack / (distanceBlack * distanceBlack + 1)) / 2; // ������������ ���� ����������
                  return gravity * (gravityWhite + gravityBlack); // �������� ���� ����������
              },
              standardPhysics);
        }
        public static IEnumerable<Level> CreateLevels() { // ��� ������� ������
            yield return CreateLevelZero(); // ������� ����������
            yield return CreateLevelHeavy(); // ���������� ���������� 0.9, ������������ ����
            yield return CreateLevelUp(); // ���������� ���������� �����
            yield return CreateLevelWhiteHole(); // ���������� ���������� �� ����
            yield return CreateLevelBlackHole(); // � �������� ������� ��������
            yield return CreateLevelBlackAndWhite(); // C������� �������������� ���������� WhiteHole � BlackHole
        }
    }
}
