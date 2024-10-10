using UnityEngine;

namespace GameCore.GameControllers
{
    public class ScatterCalculator
    {
        public Vector2 ApplyScatter(Vector2 originalDir, float speedRatio, float maxSpeedRatio, float maxScatterAngle)
        {
            
            // Прямопропорционально зависимости от коэффициента скорости
            float scatterAngle = Mathf.Lerp(0, maxScatterAngle, (speedRatio - 1) / (maxSpeedRatio - 1));

            // Выбираем случайный угол в диапазоне от -scatterAngle до scatterAngle
            float randomAngle = Random.Range(-scatterAngle, scatterAngle);

            // Преобразуем угол в радианы для поворота вектора
            float angleInRadians = randomAngle * Mathf.Deg2Rad;

            return GetDir(angleInRadians, originalDir);
        }

        public Vector2 GetDir(float angleInRadians, Vector2 originalDir)
        {
            float cosTheta = Mathf.Cos(angleInRadians);
            float sinTheta = Mathf.Sin(angleInRadians);
            
            Vector2 newDir = new Vector2(
                originalDir.x * cosTheta - originalDir.y * sinTheta,
                originalDir.x * sinTheta + originalDir.y * cosTheta
            );

            return newDir;
        }
    }
}