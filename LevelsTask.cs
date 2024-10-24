using System;
using System.Collections.Generic;

namespace func_rocket { // пространство имён с описанием игровых объектов и задач
    public class LevelsTask { // класс, который генерирует игровые уровни

        static readonly Physics standardPhysics = new(); // статический экземпляр класса Physics с параметрами по умолчанию

        public static Level CreateLevelZero() { // объект класса Level, описывающий игровой уровень Zero
            return new Level("Zero",
              new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI), // объект Rocket с начальными координатами, начальной скоростью и углом поворота
              new Vector(600, 200), // положение цели на игровом поле
              (size, v) => Vector.Zero, standardPhysics); // воздействие на ракету не производится
        }
        public static Level CreateLevelHeavy() { // объект класса Level, описывающий игровой уровень Heavy
            return new Level("Heavy",
              new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI), // объект Rocket с начальными координатами, начальной скоростью и углом поворота
              new Vector(600, 200), // положение цели на игровом поле
              (size, v) => new Vector(0, 0.9), standardPhysics); // действует гравитация
        }

        public static Level CreateLevelUp() { // объект класса Level, описывающий игровой уровень Up
            return new Level("Up",
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI), // объект Rocket с начальными координатами, начальной скоростью и углом поворота
                new Vector(700, 500), // положение цели на игровом поле
                (size, v) => { // принимает размер игрового поля и текущее положение ракеты
                    var d = size.Y - v.Y; // вычисление длины отрезка между положением цели и нижней границей игрового поля
                    return new Vector(0, -300.0 / (d + 300.0)); // расчет вектора, задающего скорость цели 
                },
                standardPhysics);
        }

        public static Level CreateLevelWhiteHole() { // объект класса Level, описывающий игровой уровень CreateLevelWhiteHole
            return new Level("WhiteHole",
              new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI), // объект Rocket с начальными координатами, начальной скоростью и углом поворота
              new Vector(600, 400), // положение цели на игровом поле
              (size, v) => { // принимает размер игрового поля и текущее положение ракеты
                  Vector gravityDirection = (new Vector(600, 400) - v).Normalize(); // вектор направления на центр притяжения
                  double distance = (new Vector(600, 400) - v).Length; // расстояния между центром притяжения и текущим положением ракеты
                  double gravity = 140 * distance / (distance * distance + 1); // вычисление гравитационной силы
                  return gravity * gravityDirection; // вектор, задающий гравитационное ускорение
              },
              standardPhysics);
        }

        public static Level CreateLevelBlackHole() { // объект класса Level, описывающий игровой уровень BlackHole
            return new Level("BlackHole",
              new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI), // объект Rocket с начальными координатами, начальной скоростью и углом поворота
              new Vector(700, 500), // положение цели на игровом поле
              (size, v) => { // принимает размер игрового поля и текущее положение ракеты
                  Vector anomalyPos = (new Vector(200, 500) + new Vector(700, 500)) / 2; // координаты центра аномалии
                  Vector gravityDirection = (anomalyPos - v).Normalize(); // направления на центр притяжения
                  double distance = (anomalyPos - v).Length; // расстояния до центра притяжения
                  double gravity = 300 * distance / (distance * distance + 1); // вычисление гравитационной силы
                  return gravity * gravityDirection; // вычисление вектора гравитационного ускорения
              },
              standardPhysics);
        }

        public static Level CreateLevelBlackAndWhite() { // объект класса Level, описывающий игровой уровень BlackAndWhite
            return new Level("BlackAndWhite",
              new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI), // объект Rocket с начальными координатами, начальной скоростью и углом поворота
              new Vector(600, 400), // положение цели на игровом поле
              (size, v) => { // принимает размер игрового поля и текущее положение ракеты
                  Vector gravityWhite = (new Vector(600, 400) - v).Normalize(); // вектор гравитации для белых объектов
                  Vector gravityBlack = ((new Vector(200, 500) + new Vector(700, 500)) / 2 - v).Normalize(); // вектор гравитации для черных объектов
                  double distanceWhite = (new Vector(600, 400) - v).Length; // расстояние до белых объектов
                  double distanceBlack = ((new Vector(200, 500) + new Vector(700, 500)) / 2 - v).Length; // расстояние до черных объектов
                  double gravity = (140 * distanceWhite / (distanceWhite * distanceWhite + 1) + 300 * distanceBlack / (distanceBlack * distanceBlack + 1)) / 2; // Рассчитываем силу гравитации
                  return gravity * (gravityWhite + gravityBlack); // итоговая силу гравитации
              },
              standardPhysics);
        }
        public static IEnumerable<Level> CreateLevels() { // все игровые уровни
            yield return CreateLevelZero(); // Нулевая гравитация
            yield return CreateLevelHeavy(); // Постоянная гравитация 0.9, направленная вниз
            yield return CreateLevelUp(); // Гравитация направлена вверх
            yield return CreateLevelWhiteHole(); // Гравитация направлена от цели
            yield return CreateLevelBlackHole(); // В середине отрезка аномалия
            yield return CreateLevelBlackAndWhite(); // Cреднеее арифметическое гравитаций WhiteHole и BlackHole
        }
    }
}
