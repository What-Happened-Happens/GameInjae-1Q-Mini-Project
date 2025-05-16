using UnityEngine;

public class BackGroundLayer : MonoBehaviour
{
    // �з����� ȿ���� �� ��� ��� ������Ʈ�� (��, �� ��� ����)
    public Transform[] backgrounds;

    // �з����� ȿ���� �ε巯�� ���� (���� Ŭ���� �ε巴�� ������)
    public float smoothing = 1f;

    // �� ����� ī�޶� �̵��� �󸶳� �������� �����ϴ� ���� (Z�� ���)
    private float[] parallaxScales;

    // ���� ī�޶��� Transform ����
    private Transform cam;

    // ���� �������� ī�޶� ��ġ
    private Vector3 previousCamPos;    

    //����� ��� ������ ������ ������ �� �ִ� ��!!
    public float bGRelativePos;

    // Start()���� ���� ȣ��Ǹ�, ���� ������ ������ ����
    void Awake()
    {
        // ���� ī�޶��� Transform ��������
        this.cam = Camera.main.transform;
    }

    // �ʱ� ���� (���� ���� �� ȣ���)
    void Start()
    {
        bGRelativePos = 20f;
        // ���� �������� ī�޶� ��ġ�� ���� ��ġ�� �ʱ�ȭ
        this.previousCamPos = this.cam.position;   

        // ��� ����ŭ parallaxScales �迭 �ʱ�ȭ
        this.parallaxScales = new float[this.backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (this.backgrounds[i].position.z > 0)
            {
                // Z���� ���� ������ ���� ���� (�ּ��� ������ �����̵���)
                this.parallaxScales[i] = backgrounds[i].position.z * -bGRelativePos;
            }
            else if(this.backgrounds[i].position.z < 0)  // �̰Ŵ� ���� �ʿ�!!
            {
                this.parallaxScales[i] = backgrounds[i].position.z * -bGRelativePos * (-1f);
            }
            
        }
    }

    // �� �����Ӹ��� �����
    void Update()
    {
        for (int i = 0; i < this.backgrounds.Length; i++)
        {
            // ���� �����Ӱ� ���� ������ ���� ī�޶��� �̵��� * ��� ���� ���
            Vector2 parallax = (this.previousCamPos - this.cam.position) * this.parallaxScales[i];

            // ����� ��ǥ ��ġ ��� (X, Y ���� ����)
            float backgroundTargetPosX = this.backgrounds[i].position.x + parallax.x;
            float backgroundTargetPosY = this.backgrounds[i].position.y + parallax.y;

            // ���� �̵� ��ġ ���� (Z�� �״�� ����)
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, this.backgrounds[i].position.z);

            // ���� ��ġ�� ��ǥ ��ġ ���̸� �ε巴�� �̵� (Lerp ���)
            this.backgrounds[i].position = Vector3.Lerp(this.backgrounds[i].position, backgroundTargetPos, this.smoothing * Time.deltaTime);
        }

        // ���� �������� ���� ���� ī�޶� ��ġ ����
        this.previousCamPos = this.cam.position;
    }
}
