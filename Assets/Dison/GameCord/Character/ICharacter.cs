using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacter 
{
    protected ICharacterAttr m_Attribute = null;// 數值  (帶入角色應等級而改變的數值)
    protected int m_AttrID = 0;         // 使用的角色屬性編號 (看能不能改到IPlayerCharacter)

	/// <summary>
	/// 攻擊特效(prefab)
	/// </summary>
	protected GameObject obj_preAttackVFX;

	/// <summary>
	/// 生成物件後的特效
	/// </summary>
	protected GameObject obj_InstAttackVFX;

	/// <summary>
	/// 攻擊音效
	/// </summary>
	protected AudioClip atkSoundFx;

    public int GetAttrID()
    {
        return m_AttrID;
    }

	/// <summary>
	/// 設定角色數值
	/// </summary>
	/// <param name="CharacterAttr"></param>
	public virtual void SetCharacterAttr(ICharacterAttr CharacterAttr)
	{
		// 設定
		m_Attribute = CharacterAttr;
		m_Attribute.InitAttr();	
	}


	public int GetPlayerHP()
    {
		return m_Attribute.GetPlayerHP();

	}

	/// <summary>
	/// 取得角色現在最大的HP
	/// </summary>
	/// <returns></returns>
	public int GetNowHP()
    {
		return m_Attribute.GetNowHP();

	}

	/// <summary>
	/// 取得角色現在最大的ATK
	/// </summary>
	/// <returns></returns>
	public int GetNowATK()
    {
		return m_Attribute.GetNowATK();
	}


	public void SetDamage(int damage)
    {
		m_Attribute.SetDamage(damage);
	}

	public int GetDamage()
    {
		return m_Attribute.GetDamage();
	}

	/// <summary>
	/// 攻擊目標
	/// </summary>
	/// <param name="target">目標</param>
	public void Attack(ICharacter target)
    {
		int damage = GetDamage();
		target.Hurt(damage);
	}

	/// <summary>
	/// 受到傷害
	/// </summary>
	/// <param name="damage">得到的傷害</param>
	public void Hurt(int damage)
    {
		m_Attribute.Hurt(damage);

	}

    #region 特效
	public void SetAttackVFX(GameObject obj_preVFX)
    {
		obj_preAttackVFX = obj_preVFX;
    }

	/// <summary>
	/// 生成攻擊特效(Instantiate)
	/// </summary>
	public GameObject InstAttackVFX(Vector3 VFXpos, Vector3 targetAngle, Vector3 battle3targetAngle, int nowBattle)
    {
		if (nowBattle == 3)
        {
			obj_InstAttackVFX = Object.Instantiate(obj_preAttackVFX, VFXpos, Quaternion.Euler(battle3targetAngle));
		}
		else
        {
			obj_InstAttackVFX = Object.Instantiate(obj_preAttackVFX, VFXpos, Quaternion.Euler(targetAngle));
		}
		
		return obj_InstAttackVFX;
	}
    #endregion

    #region 音效
    /// <summary>
    /// 設定攻擊音效
    /// </summary>
    /// <returns></returns>
    public void SetAtkSoundFx(AudioClip atkSoundFx)
    {
		this.atkSoundFx = atkSoundFx;

	}

	/// <summary>
	/// 取得攻擊音效
	/// </summary>
	/// <returns></returns>
	public AudioClip GetAtkSoundFx()
    {
		return atkSoundFx;

	}
	#endregion
}
