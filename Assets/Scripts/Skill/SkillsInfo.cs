using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInfo : MonoBehaviour
{

    public static SkillsInfo _instance;
    public TextAsset skillsInfoText;

    private Dictionary<int, SkillInfo> skillInfoDict = new Dictionary<int, SkillInfo>();

    void Awake()
    {
        _instance = this;
        InitSkillInfoDict();//初始化技能信息字典
    }

    //我们可以通过在这个方法，根据id查找到一个技能信息
    public SkillInfo GetSkillInfoById(int id)
    {
        SkillInfo info = null;
        skillInfoDict.TryGetValue(id, out info);

        return info;
    }

    //初始化技能信息字典
    void InitSkillInfoDict()
    {
        string text = skillsInfoText.text;
        string[] skillinfoArray = text.Split('\n');
        foreach (string skillinfoStr in skillinfoArray)
        {
            string[] pa = skillinfoStr.Split(',');
            SkillInfo info = new SkillInfo();
            info.Id = int.Parse(pa[0]);
            info.Name = pa[1];
            info.Icon_name = pa[2];
            info.Des = pa[3];
            string str_applytype = pa[4];
            switch (str_applytype)
            {
                case "Passive":
                    info.ApplyType = ApplyType.Passive;
                    break;
                case "Buff":
                    info.ApplyType = ApplyType.Buff;
                    break;
                case "SingleTarget":
                    info.ApplyType = ApplyType.SingleTarget;
                    break;
                case "MultiTarget":
                    info.ApplyType = ApplyType.MultiTarget;
                    break;
            }
            string str_applypro = pa[5];
            switch (str_applypro)
            {
                case "Attack":
                    info.ApplyProperty = ApplyProperty.Attack;
                    break;
                case "Def":
                    info.ApplyProperty = ApplyProperty.Def;
                    break;
                case "Speed":
                    info.ApplyProperty = ApplyProperty.Speed;
                    break;
                case "AttackSpeed":
                    info.ApplyProperty = ApplyProperty.AttackSpeed;
                    break;
                case "HP":
                    info.ApplyProperty = ApplyProperty.HP;
                    break;
                case "MP":
                    info.ApplyProperty = ApplyProperty.MP;
                    break;
            }
            info.ApplyValue = int.Parse(pa[6]);
            info.ApplyTime = int.Parse(pa[7]);
            info.Mp = int.Parse(pa[8]);
            info.ColdTime = int.Parse(pa[9]);
            switch (pa[10])
            {
                case "Swordman":
                    info.ApplicableRole = ApplicableRole.Swordman;
                    break;
                case "Magician":
                    info.ApplicableRole = ApplicableRole.Magician;
                    break;
            }
            info.Level = int.Parse(pa[11]);
            switch (pa[12])
            {
                case "Self":
                    info.ReleaseType = ReleaseType.Self;
                    break;
                case "Enemy":
                    info.ReleaseType = ReleaseType.Enemy;
                    break;
                case "Position":
                    info.ReleaseType = ReleaseType.Position;
                    break;
            }
            info.Distance = float.Parse(pa[13]);
            info.Efx_name = pa[14];
            info.Aniname = pa[15];
            info.Anitime = float.Parse(pa[16]);
            skillInfoDict.Add(info.Id, info);
        }
    }
}
    //适用角色
    public enum ApplicableRole
    {
        Swordman,
        Magician
    }
    //作用类型
    public enum ApplyType
    {
        Passive,
        Buff,
        SingleTarget,
        MultiTarget
    }
    //作用属性
    public enum ApplyProperty
    {
        Attack,
        Def,
        Speed,
        AttackSpeed,
        HP,
        MP
    }
    //释放类型
    public enum ReleaseType
    {
        Self,
        Enemy,
        Position
    }


//技能信息
public class SkillInfo
{
    private int id;
    private string name;
    private string icon_name;
    private string des;
    private ApplyType applyType;
    private ApplyProperty applyProperty;
    private int applyValue;
    private int applyTime;
    private int mp;
    private int coldTime;
    private ApplicableRole applicableRole;
    private int level;
    private ReleaseType releaseType;
    private float distance;
    private string efx_name;
    private string aniname;
    private float anitime = 0;

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Icon_name
    {
        get
        {
            return icon_name;
        }

        set
        {
            icon_name = value;
        }
    }

    public string Des
    {
        get
        {
            return des;
        }

        set
        {
            des = value;
        }
    }

    public ApplyType ApplyType
    {
        get
        {
            return applyType;
        }

        set
        {
            applyType = value;
        }
    }

    public ApplyProperty ApplyProperty
    {
        get
        {
            return applyProperty;
        }

        set
        {
            applyProperty = value;
        }
    }

    public int ApplyValue
    {
        get
        {
            return applyValue;
        }

        set
        {
            applyValue = value;
        }
    }

    public int ApplyTime
    {
        get
        {
            return applyTime;
        }

        set
        {
            applyTime = value;
        }
    }

    public int Mp
    {
        get
        {
            return mp;
        }

        set
        {
            mp = value;
        }
    }

    public int ColdTime
    {
        get
        {
            return coldTime;
        }

        set
        {
            coldTime = value;
        }
    }

    public ApplicableRole ApplicableRole
    {
        get
        {
            return applicableRole;
        }

        set
        {
            applicableRole = value;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    public ReleaseType ReleaseType
    {
        get
        {
            return releaseType;
        }

        set
        {
            releaseType = value;
        }
    }

    public float Distance
    {
        get
        {
            return distance;
        }

        set
        {
            distance = value;
        }
    }

    public string Efx_name
    {
        get
        {
            return efx_name;
        }

        set
        {
            efx_name = value;
        }
    }

    public string Aniname
    {
        get
        {
            return aniname;
        }

        set
        {
            aniname = value;
        }
    }

    public float Anitime
    {
        get
        {
            return anitime;
        }

        set
        {
            anitime = value;
        }
    }
}
