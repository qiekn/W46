using UnityEngine;

public class DrawViewArea : MonoBehaviour {
  public GameObject enemyObject;
  public Material matGreen;
  public Material matRed;

  private Enemy enemy;
  private LineRenderer lineRenderer;

  float radius = 5f;       // 视野半径
  float field = 120f;     // 视野角度
  int segments = 120 + 2;  // 细分数量（增加两点：扇形的起点和终点）

  private void Start() {
    lineRenderer = GetComponent<LineRenderer>();
    lineRenderer.positionCount = segments;

    enemy = enemyObject.GetComponent<Enemy>();

    radius = enemy.SightDistance;
    field = enemy.FieldOfView;
    segments = (int)field + 2;
    //Debug.Log($"radius={radius}, field={field}, segments={segments}");
  }

  private void Update() {
    Draw();
  }


  private void Draw() {
    Vector3 origin = transform.position;
    origin.y = 0.1f; // 使得扇形贴近地面绘制
    float halfField = field / 2;
    float angleStep = field / (segments - 2); // 角度步长
    Quaternion rotation = transform.rotation; // 获取敌人自身的朝向

    // 修改扇形材质
    if (enemy.CanSeePlayer) {
      lineRenderer.material = matRed;
    } else {
      lineRenderer.material = matGreen;
    }
    // 设置起点和终点
    lineRenderer.SetPosition(0, origin);
    lineRenderer.SetPosition(segments - 1, origin);

    for (int i = 1; i < segments - 1; i++) {
      // 计算当前角度
      float angle = -halfField + angleStep * (i - 1);

      // 将角度旋转到当前朝向的方向
      Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;
      Vector3 point = origin + direction.normalized * radius;

      lineRenderer.SetPosition(i, point);
    }
  }
}