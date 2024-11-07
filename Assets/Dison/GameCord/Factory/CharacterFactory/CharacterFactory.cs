using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : ICharacterFactory
{
    // 角色建立指導者
    private CharacterBuilderSystem m_BuilderDirector = new CharacterBuilderSystem(TowerOfAdventureGame.Inst);

    public override IPlayerCharacter CreatePlayerCharacter(PlayerCharacter playerCharacter ,int Lv)
    {
        PlayerCharacterBuilderParam playerCharacterParam = new PlayerCharacterBuilderParam();

        switch (playerCharacter)
        {
            case PlayerCharacter.Alice:
                playerCharacterParam.NewCharacter = new Alice(); //艾莉絲
                break;
            case PlayerCharacter.Rogritte:
                playerCharacterParam.NewCharacter = new Rogritte(); //羅格莉特
                break;
            case PlayerCharacter.Keli:
                playerCharacterParam.NewCharacter = new Keli(); //可莉
                break;
            case PlayerCharacter.LonelySnow:
                playerCharacterParam.NewCharacter = new LonelySnow(); //孤獨雪
                break;
            case PlayerCharacter.Yuna:
                playerCharacterParam.NewCharacter = new Yuna(); //悠娜
                break;
            default:
                Debug.LogWarning("CreateSoldier:無法建立[" + playerCharacter + "]");
                return null;
        }

        if (playerCharacterParam.NewCharacter == null)
            return null;

        playerCharacterParam.Lv = Lv;  //創造出來的角色等級要幾等

        //設定參數
        PlayerCharacterBuilder thePlayerCharacterBuilder = new PlayerCharacterBuilder();
        thePlayerCharacterBuilder.SetBuildParam(playerCharacterParam);

        //產生角色
        m_BuilderDirector.Construct(thePlayerCharacterBuilder, playerCharacter);
        return playerCharacterParam.NewCharacter as IPlayerCharacter;
    }


    public override IPlayerCharacter CreatePlayerCharacter(List<IPlayerCharacter> list_playerTeamMember)
    {
        Debug.Log("開始創造玩家");
        PlayerBuilderParam playerParam = new PlayerBuilderParam();
        playerParam.NewCharacter = new Player();

        //設定參數
        PlayerBuilder thePlayerBuilder = new PlayerBuilder();
        thePlayerBuilder.SetBuildParam(playerParam);

        //產生角色
        m_BuilderDirector.Construct(thePlayerBuilder, list_playerTeamMember);
        return playerParam.NewCharacter as IPlayerCharacter;
    }


    public override IEnemyCharacter CreateEnemyCharacter(EnemyCharacter enemyCharacter, int Lv) 
    {
        EnemyCharacterBuilderParam enemyCharacterParam = new EnemyCharacterBuilderParam();

        switch (enemyCharacter)
        {
            case EnemyCharacter.SkeletonSoldier:
                enemyCharacterParam.NewCharacter = new SkeletonSoldier();  //骷髏士兵
                break;
            case EnemyCharacter.AngryScorpion:
                enemyCharacterParam.NewCharacter = new AngryScorpion();  //憤怒蠍子
                break;
            case EnemyCharacter.Tauren:
                enemyCharacterParam.NewCharacter = new Tauren();  //牛頭人
                break;
            default:
                Debug.LogWarning("CreateSoldier:無法建立[" + enemyCharacter + "]");
                return null;
        }

        if (enemyCharacterParam.NewCharacter == null)
            return null;

        enemyCharacterParam.Lv = Lv;  //創造出來的角色等級要幾等

        //設定參數
        EnemyCharacterBuilder theEnemyCharacterBuilder = new EnemyCharacterBuilder();
        theEnemyCharacterBuilder.SetBuildParam(enemyCharacterParam);


        //產生角色
        m_BuilderDirector.Construct(theEnemyCharacterBuilder, enemyCharacter);
        return enemyCharacterParam.NewCharacter as IEnemyCharacter;
    }

    
}
