
public interface ITower 
{
    int GetTowerCost();
    bool GetIsBuffed();
    void SetIsBuffed(bool buffed);
    void AddDamageBuff(float dmgAmt);
}
