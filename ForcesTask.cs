//В этой задаче в классе ForcesTask нужно реализовать три вспомогательных метода, преобразующих одни делегаты в другие.
//Чтобы лучше понимать зачем эти методы нужны, изучите проект, в частности места использования этих методов. Это будет полезно и для следующих заданий.
//После выполнения этого задания, при запуске проекта Каракуля должен летать и управляться клавишами A и D.

//namespace func_rocket;

//public class ForcesTask
//{
//	/// <summary>
//	/// Создает делегат, возвращающий по ракете вектор силы тяги двигателей этой ракеты.
//	/// Сила тяги направлена вдоль ракеты и равна по модулю forceValue.
//	/// </summary>
//	public static RocketForce GetThrustForce(double forceValue) => r => Vector.Zero;

//	/// <summary>
//	/// Преобразует делегат силы гравитации, в делегат силы, действующей на ракету
//	/// </summary>
//	public static RocketForce ConvertGravityToForce(Gravity gravity, Vector spaceSize) => r => Vector.Zero;

//	/// <summary>
//	/// Суммирует все переданные силы, действующие на ракету, и возвращает суммарную силу.
//	/// </summary>
//	public static RocketForce Sum(params RocketForce[] forces) => forces[0];
//}


using System;

namespace func_rocket;

public class ForcesTask { // методы для вычисления сил, действующих на ракету
    public static RocketForce GetThrustForce(double forceValue) { // сила тяги, направленная в направлении полета ракеты
        return r => new Vector(forceValue * Math.Cos(r.Direction), forceValue * Math.Sin(r.Direction)); // вектор, указывающий направление силы и ее вели
    }

    public static RocketForce ConvertGravityToForce(Gravity gravity, Vector spaceSize) { // преобразует силу гравитации в силу, действующую на ракету
        return r => gravity(spaceSize, r.Location); // вектор, указывающий направление силы и ее величину.
    }

    public static RocketForce Sum(params RocketForce[] forces) { // вычисляет суммарную силу, действующую на ракету, складывая переданные в него силы
        return r => {
            Vector totalForce = Vector.Zero; // cуммарный вектор силы, направленной на ракету

            foreach (var force in forces) { // по всем переданным силам
                Vector forceVector = force(r); // вычисляем вектор силы, действующей на ракету
                totalForce += forceVector; // складываем векторы сил, действующих на ракету
            }

            return totalForce; // Возвращаем суммарный вектор силы.
        };
    }
}