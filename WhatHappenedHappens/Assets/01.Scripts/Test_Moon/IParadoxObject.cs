

// [ ���� ������ ���� �� ���� ����� �����ϴ� �������̽� ]
//
public interface IParadoxObject
{
    ObjectSnapshot CreateSnapshot();

    void RestoreSnapshot(ObjectSnapshot snapshot);
}
