using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterBuilderParam : ICharacterBuildParam
{
    public int Lv;
}

public class PlayerCharacterBuilder : ICharacterBuilder
{
    public PlayerCharacterBuilderParam m_BuildParam = null;
    public override void SetBuildParam(ICharacterBuildParam theParam)
    {
        m_BuildParam = theParam as PlayerCharacterBuilderParam;
    }

    /// <summary>
    /// 設定角色數值
    /// </summary>
    public override void SetCharacterAttr()
    {
        IAttrFactory theAttrFactory = TOAFactory.GetAttrFactory();
        int AttrID = m_BuildParam.NewCharacter.GetAttrID();
        PlayerCharacterAttr thePlayerCharacterAttr = theAttrFactory.GetPlayerCharacterAttr(AttrID); //根據數值ID來給你相對應的角色數值

        thePlayerCharacterAttr.SetAttrStrategy(new PlayerCharacterAttrStrategy()); //把數值計算方法記下來

        // 設定等級
        thePlayerCharacterAttr.SetPlayerCharacterLv(m_BuildParam.Lv); //從外面輸入的參數

        // 設定給角色
        m_BuildParam.NewCharacter.SetCharacterAttr(thePlayerCharacterAttr); //角色把數值記下來，並且在對數值進行初始化。
    }

    public override void SetCharacterAttr(List<IPlayerCharacter> list_PlayerTeamMember)
    {
        //這是PlayerBulider要做的事情
    }

    /// <summary>
    /// 設定攻擊特效
    /// </summary>
    /// <param name="playerCharacter"></param>
    public override void SetAttackVFX(PlayerCharacter playerCharacter)
    {
        switch(playerCharacter)
        {
            case PlayerCharacter.Alice:
                //取得特效資源
                GameObject obj_preDarkVFX = Resources.Load<GameObject>("Prefab/VFX/Projectile 1");

                //設定給角色
                m_BuildParam.NewCharacter.SetAttackVFX(obj_preDarkVFX);
                break;
            case PlayerCharacter.Rogritte:
                //取得特效資源
                GameObject obj_preFireVFX = Resources.Load<GameObject>("Prefab/VFX/Projectile 8");

                //設定給角色
                m_BuildParam.NewCharacter.SetAttackVFX(obj_preFireVFX);
                break;
            case PlayerCharacter.Keli:
                //取得特效資源
                GameObject obj_preWoodVFX = Resources.Load<GameObject>("Prefab/VFX/Projectile 10");

                //設定給角色
                m_BuildParam.NewCharacter.SetAttackVFX(obj_preWoodVFX);
                break;
            case PlayerCharacter.LonelySnow:
                //取得特效資源
                GameObject obj_preWaterVFX = Resources.Load<GameObject>("Prefab/VFX/Projectile 9");

                //設定給角色
                m_BuildParam.NewCharacter.SetAttackVFX(obj_preWaterVFX);
                break;
            case PlayerCharacter.Yuna:
                //取得特效資源
                GameObject obj_preLightVFX = Resources.Load<GameObject>("Prefab/VFX/Projectile 15");

                //設定給角色
                m_BuildParam.NewCharacter.SetAttackVFX(obj_preLightVFX);
                break;
        }
    }

    public override void SetAttackVFX(EnemyCharacter enemyCharacter)
    {
        //角色不用做這件事情
    }

    /// <summary>
    /// 設定攻擊音效
    /// </summary>
    /// <param name="playerCharacter"></param>
    public override void SetAtkSoundFx(PlayerCharacter playerCharacter)
    {
        switch (playerCharacter)
        {
            case PlayerCharacter.Alice:
                //取得特效資源
                AudioClip darkSoundFX = Resources.Load<AudioClip>("AtkSoundFx/Dark");

                //設定給角色
                m_BuildParam.NewCharacter.SetAtkSoundFx(darkSoundFX);
                break;
            case PlayerCharacter.Rogritte:
                //取得特效資源
                AudioClip fireSoundFX = Resources.Load<AudioClip>("AtkSoundFx/Fire");

                //設定給角色
                m_BuildParam.NewCharacter.SetAtkSoundFx(fireSoundFX);
                break;
            case PlayerCharacter.Keli:
                //取得特效資源
                AudioClip woodSoundFX = Resources.Load<AudioClip>("AtkSoundFx/Wood");

                //設定給角色
                m_BuildParam.NewCharacter.SetAtkSoundFx(woodSoundFX);
                break;
            case PlayerCharacter.LonelySnow:
                //取得特效資源
                AudioClip waterSoundFX = Resources.Load<AudioClip>("AtkSoundFx/Water");

                //設定給角色
                m_BuildParam.NewCharacter.SetAtkSoundFx(waterSoundFX);
                break;
            case PlayerCharacter.Yuna:
                //取得特效資源
                AudioClip lightSoundFX = Resources.Load<AudioClip>("AtkSoundFx/Light");

                //設定給角色
                m_BuildParam.NewCharacter.SetAtkSoundFx(lightSoundFX);
                break;
        }
    }

    public override void SetAtkSoundFx(EnemyCharacter enemyCharacter)
    {
        //PlayerCharacterBuilder不用做的事情
    }
}
