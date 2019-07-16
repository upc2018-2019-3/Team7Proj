using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// 学生管理类
/// </summary>
public class PersonMgr
{
	public PersonMgr()
	{
        
	}

    #region　函数UpdatePerson | 更新Person信息
    /// <summary>
    /// 更新Person类信息
    /// </summary>
    /// <param name="person">学生管理类参数</param>
    /// <returns>更新成功返回true 否则返回false</returns>
    public bool UpdatePerson(Person person)
    {
        string strSQL = "";
        SqlDataAdapter sa = null;
        SqlCommandBuilder builder = null;
        DataRow row = null;
        DataTable dt = null;
        bool returnValue = false;
        if (person != null)
        {
            if (person.PersonID == "")
            {
                strSQL = "SELECT Top 0 * FROM Sys_Person";
            }
            else
            {
                strSQL = "SELECT Top 1 * FROM Sys_Person WHERE PersonID = '" + person.PersonID + "'";
            }

            dt = CMMgr.GetDataTable(strSQL);

            if (dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
            }
            else
            {
                row = dt.NewRow();
            }

            row["PersonID"] = person.PersonID;
            row["PersonName"] = person.PersonName;
            row["Password"] = person.Password;            
            row["Sex"] = person.Sex;
            row["Birthday"] = person.Birthday;
            row["ClassInfoID"] = person.ClassInfo.ClassInfoID;
            row["SpecID"] = person.Spec.SpecID;
           


            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(row);
            }

            SqlConnection conn = CMMgr.GetConnection();
            using (sa = new SqlDataAdapter("SELECT Top 0 * FROM Sys_Person", conn))
            {
                try
                {
                    builder = new SqlCommandBuilder(sa);
                    sa.Update(dt);
                    returnValue = true;
                }
                finally
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        return returnValue;
    }
    #endregion

    #region　函数GetPerson | 获取Person信息
    /// <summary>
    /// 获取一条Person类信息
    /// </summary>
    /// <param name="PersonID">登录编号</param>
    /// <returns>一条Person类记录</returns>
    public Person GetPerson(string PersonID)
    {
        Person person = new Person();
        string strSQL = "SELECT Top 1 * FROM Sys_Person WHERE PersonID = '" + PersonID + "'";
        DataTable dt = CMMgr.GetDataTable(strSQL);
        ClassInfoMgr cMgr = new ClassInfoMgr();
        SpecMgr sMgr = new SpecMgr();

        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            person.PersonID = row["PersonID"].ToString();
            person.PersonName = row["PersonName"].ToString();
            person.Password = row["Password"].ToString();            
            person.Sex = row["Sex"].ToString();
            person.Birthday = row["Birthday"].ToString();
            person.ClassInfo = cMgr.GetClassInfo(row["ClassInfoID"].ToString());
            person.Spec = sMgr.GetSpec(row["SpecID"].ToString());
            
            return person;
        }
        else
        {
            return person;
        }
    }
    #endregion

    #region 函数ExistsPerson | 判断一个学生登录帐号是否存在
    /// <summary>
    /// 函数ExistsPerson | 判断一个学生登录帐号是否存在
    /// </summary>
    /// <param name="PersonID">学生帐号</param>
    /// <returns>存在返回true不存在返回false</returns>
    public bool ExistsPerson(string PersonID)
    {
        ArrayList arList = new ArrayList();
        SqlParameter Param = null;
        string strSQL = "SELECT Top 1 PersonID FROM Sys_Person WHERE PersonID = @PersonID";

        Param = new SqlParameter("PersonID", SqlDbType.NVarChar);
        Param.Value = PersonID;
        arList.Add(Param);


        if (CMMgr.GetDataTable(strSQL, arList).Rows.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion

    #region　函数DelPerson | 删除Person信息
    /// <summary>
    /// 删除Person类信息
    /// </summary>
    /// <param name="PersonID">登录编号参数</param>
    public void DelPerson(string PersonID)
    {
        string strSQL = "DELETE FROM Sys_Person WHERE PersonID = '" + PersonID + "'";
        CMMgr.ExecuteNonQuery(strSQL);
    }

    /// <summary>
    /// 删除Person类信息
    /// </summary>
    /// <param name="person">Person类</param>
    public void DelPerson(Person person)
    {
        this.DelPerson(person.PersonID);
    }
    #endregion

    #region 函数 GetPersonList | 获取学生信息列表
    /// <summary>
    /// 函数 GetPersonList | 获取学生信息列表
    /// </summary>
    /// <param name="PersonID">登录名称</param>
    /// <param name="PersonName">真实名称</param>
    /// <returns>学生列表</returns>
    public DataTable GetPersonList(string PersonID, string PersonName)
    {
        string strSQL = "SELECT *,(SELECT SpecName FROM Sys_Spec WHERE SpecID = Sys_Person.SpecID) AS SpecName,(SELECT ClassInfoName FROM Sys_ClassInfo WHERE ClassInfoID = Sys_Person.ClassInfoID) AS ClassInfoName FROM Sys_Person WHERE "
                      + "PersonName Like '%" + PersonName + "%' "
                      + "AND PersonID Like '%" + PersonID + "%' ";
        return CMMgr.GetDataTable(strSQL);
    }

    /// <summary>
    /// 函数 GetPersonList | 获取学生信息列表
    /// </summary>
    /// <param name="PersonID">登录名称</param>
    /// <param name="PersonName">真实名称</param>
    /// <returns>学生列表</returns>
    public DataTable GetPersonList(string PersonID, string PersonName,int SpecID,int ClassInfoID)
    {
        string strSQL = "SELECT *,(SELECT SpecName FROM Sys_Spec WHERE SpecID = Sys_Person.SpecID) AS SpecName,(SELECT ClassInfoName FROM Sys_ClassInfo WHERE ClassInfoID = Sys_Person.ClassInfoID) AS ClassInfoName FROM Sys_Person WHERE "
                      + "PersonName Like '%" + PersonName + "%' "
                      + "AND PersonID Like '%" + PersonID + "%' ";
        if (SpecID != -1)
        {
            strSQL += "AND SpecID = " + SpecID.ToString() + " ";
        }

        if (ClassInfoID != -1)
        {
            strSQL += "AND ClassInfoID = " + ClassInfoID.ToString() + " ";
        }
        return CMMgr.GetDataTable(strSQL);
    }
    #endregion
}
