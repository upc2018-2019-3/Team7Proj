using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// 评定指标管理类
/// </summary>
public class AssesMgr
{
	public AssesMgr()
	{
        
	}

    #region　函数UpdateAsses | 更新Asses信息
    /// <summary>
    /// 更新Asses类信息
    /// </summary>
    /// <param name="asses">评定指标管理类参数</param>
    /// <returns>更新成功返回true 否则返回false</returns>
    public bool UpdateAsses(Asses asses)
    {
        string strSQL = "";
        SqlDataAdapter sa = null;
        SqlCommandBuilder builder = null;
        DataRow row = null;
        DataTable dt = null;
        bool returnValue = false;
        if (asses != null)
        {
            if (asses.AssesID == 0)
            {
                strSQL = "SELECT Top 0 * FROM Sys_Asses";
            }
            else
            {
                strSQL = "SELECT Top 1 * FROM Sys_Asses WHERE AssesID = " + asses.AssesID.ToString();
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

            
            row["PersonID"] = asses.Person.PersonID;
            row["GuideID"] = asses.Guide.GuideID;
            row["Guide2ID"] = asses.Guide2.Guide2ID;
            row["Scale1"] = asses.Scale1;
            row["Scale2"] = asses.Scale2;
            row["Scale3"] = asses.Scale3;
            row["Scale1Name"] = asses.Scale1Name;
            row["Scale2Name"] = asses.Scale2Name;
            row["Scale3Name"] = asses.Scale3Name;
            row["CountNumber"] = asses.CountNumber;
            row["Memo"] = asses.Memo;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(row);
            }

            SqlConnection conn = CMMgr.GetConnection();
            using (sa = new SqlDataAdapter("SELECT Top 0 * FROM Sys_Asses", conn))
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

    #region　函数GetAsses | 获取Asses信息
    /// <summary>
    /// 获取一条Asses类信息
    /// </summary>
    /// <param name="AssesID">登录编号</param>
    /// <returns>一条Asses类记录</returns>
    public Asses GetAsses(string AssesID)
    {
        Asses asses = new Asses();
        GuideMgr gMgr = new GuideMgr();
        Guide2Mgr g2Mgr = new Guide2Mgr();
        PersonMgr pMgr = new PersonMgr();

        string strSQL = "SELECT Top 1 * FROM Sys_Asses WHERE AssesID = " + AssesID;
        DataTable dt = CMMgr.GetDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            asses.AssesID = int.Parse(row["AssesID"].ToString());
            asses.Person = pMgr.GetPerson(row["PersonID"].ToString());
            asses.Guide = gMgr.GetGuide(row["GuideID"].ToString());
            asses.Guide2 = g2Mgr.GetGuide2(row["Guide2ID"].ToString());
            asses.Scale1 = int.Parse(row["Scale1"].ToString());
            asses.Scale2 = int.Parse(row["Scale2"].ToString());
            asses.Scale3 = int.Parse(row["Scale3"].ToString());
            asses.Scale1Name = row["Scale1Name"].ToString();
            asses.Scale2Name = row["Scale2Name"].ToString();
            asses.Scale3Name = row["Scale3Name"].ToString();
            asses.CountNumber = int.Parse(row["CountNumber"].ToString());
            asses.Memo = row["Memo"].ToString();

            return asses;
        }
        else
        {
            return asses;
        }
    }
    #endregion

    #region　函数DelAsses | 删除Asses信息
    /// <summary>
    /// 删除Asses类信息
    /// </summary>
    /// <param name="AssesID">登录编号参数</param>
    public void DelAsses(string AssesID)
    {
        string strSQL = "DELETE FROM Sys_Asses WHERE AssesID = " + AssesID;
        CMMgr.ExecuteNonQuery(strSQL);
    }

    public void DelAsses(int AssesID)
    {
        this.DelAsses(AssesID.ToString());
    }
    #endregion

    #region 函数 GetAssesList | 获取评定指标信息列表
    /// <summary>
    /// 函数 GetAssesList | 获取评定指标信息列表
    /// </summary>
    /// <returns>评定指标列表</returns>
    public DataTable GetAssesNumberList(string PersonID, int GuideID)
    {
        string strSQL = @"SELECT 
                            *
                            ,(SELECT GuideName FROM Sys_Guide WHERE GuideID = Sys_Asses.GuideID) AS GuideName 
                            ,(SELECT Guide2Name FROM Sys_Guide2 WHERE Guide2ID = Sys_Asses.Guide2ID) AS Guide2Name 
                            ,(SELECT PersonName FROM Sys_Person WHERE PersonID = Sys_Asses.PersonID) AS PersonName 
                            ,(SELECT Sex FROM Sys_Person WHERE PersonID = Sys_Asses.PersonID) AS Sex
                            ,(SELECT Top 1 ClassInfoName FROM Sys_ClassInfo WHERE ClassInfoID IN (SELECT ClassInfoID FROM Sys_Asses WHERE PersonID = Sys_Asses.PersonID)) AS ClassInfoName
                            ,(SELECT Top 1 SpecName FROM Sys_Spec WHERE SpecID IN (SELECT SpecID FROM Sys_Asses WHERE PersonID = Sys_Asses.PersonID)) AS SpecName
                          FROM 
                            Sys_Asses 
                          WHERE 
                            PersonID = '" + PersonID + "' ";

        if (GuideID != -1)
        {
            strSQL += "AND GuideID = " + GuideID.ToString() + " ";
        }
        return CMMgr.GetDataTable(strSQL);
    }

    /// <summary>
    /// 函数 GetAssesList | 获取评定指标信息列表
    /// </summary>
    /// <returns>评定指标列表</returns>
    public DataTable GetAssesList(string PersonID,string PersonName,int SpecID)
    {
        string strSQL = @"SELECT 
                            *
                            ,(SELECT GuideName FROM Sys_Guide WHERE GuideID = Sys_Asses.GuideID) AS GuideName 
                            ,(SELECT Guide2Name FROM Sys_Guide2 WHERE Guide2ID = Sys_Asses.Guide2ID) AS Guide2Name 
                            ,(SELECT PersonName FROM Sys_Person WHERE PersonID = Sys_Asses.PersonID) AS PersonName 
                            ,(SELECT Sex FROM Sys_Person WHERE PersonID = Sys_Asses.PersonID) AS Sex
                            ,(SELECT Top 1 ClassInfoName FROM Sys_ClassInfo WHERE ClassInfoID IN (SELECT ClassInfoID FROM Sys_Asses WHERE PersonID = Sys_Asses.PersonID)) AS ClassInfoName
                            ,(SELECT Top 1 SpecName FROM Sys_Spec WHERE SpecID IN (SELECT SpecID FROM Sys_Asses WHERE PersonID = Sys_Asses.PersonID)) AS SpecName
                            
                          FROM 
                            Sys_Asses 
                          WHERE 
                            PersonID IN (SELECT PersonID FROM Sys_Person WHERE PersonID Like '%" + PersonID + "%' AND PersonName Like '%" + PersonName + "%') ";

        if (SpecID != -1)
        {
            strSQL += "AND PersonID IN (SELECT PersonID FROM Sys_Person WHERE SpecID = " + SpecID.ToString() + ") ";
        }

        strSQL += "Order By CountNumber Desc";
        return CMMgr.GetDataTable(strSQL);
    }

    #endregion

}
