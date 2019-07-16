using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// 二级指标管理类
/// </summary>
public class Guide2Mgr
{
	public Guide2Mgr()
	{
        
	}

    #region　函数UpdateGuide2 | 更新Guide2信息
    /// <summary>
    /// 更新Guide2类信息
    /// </summary>
    /// <param name="guide2">二级指标管理类参数</param>
    /// <returns>更新成功返回true 否则返回false</returns>
    public bool UpdateGuide2(Guide2 guide2)
    {
        string strSQL = "";
        SqlDataAdapter sa = null;
        SqlCommandBuilder builder = null;
        DataRow row = null;
        DataTable dt = null;
        bool returnValue = false;
        if (guide2 != null)
        {
            if (guide2.Guide2ID == 0)
            {
                strSQL = "SELECT Top 0 * FROM Sys_Guide2";
            }
            else
            {
                strSQL = "SELECT Top 1 * FROM Sys_Guide2 WHERE Guide2ID = " + guide2.Guide2ID.ToString();
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

            row["Guide2Name"] = guide2.Guide2Name;
            row["GuideID"] = guide2.Guide.GuideID;
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(row);
            }

            SqlConnection conn = CMMgr.GetConnection();
            using (sa = new SqlDataAdapter("SELECT Top 0 * FROM Sys_Guide2", conn))
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

    #region　函数GetGuide2 | 获取Guide2信息
    /// <summary>
    /// 获取一条Guide2类信息
    /// </summary>
    /// <param name="Guide2ID">登录编号</param>
    /// <returns>一条Guide2类记录</returns>
    public Guide2 GetGuide2(string Guide2ID)
    {
        Guide2 guide2 = new Guide2();
        GuideMgr gMgr = new GuideMgr();

        string strSQL = "SELECT Top 1 * FROM Sys_Guide2 WHERE Guide2ID = " + Guide2ID;
        DataTable dt = CMMgr.GetDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            guide2.Guide2ID = int.Parse(row["Guide2ID"].ToString());
            guide2.Guide2Name = row["Guide2Name"].ToString();
            guide2.Guide = gMgr.GetGuide(row["GuideID"].ToString());
            return guide2;
        }
        else
        {
            return guide2;
        }
    }
    #endregion

    #region　函数DelGuide2 | 删除Guide2信息
    /// <summary>
    /// 删除Guide2类信息
    /// </summary>
    /// <param name="Guide2ID">登录编号参数</param>
    public void DelGuide2(string Guide2ID)
    {
        string strSQL = "DELETE FROM Sys_Guide2 WHERE Guide2ID = " + Guide2ID;
        CMMgr.ExecuteNonQuery(strSQL);
    }

    public void DelGuide2(int Guide2ID)
    {
        this.DelGuide2(Guide2ID.ToString());
    }
    #endregion

    #region 函数 GetGuide2List | 获取二级指标信息列表
    /// <summary>
    /// 函数 GetGuide2List | 获取二级指标信息列表
    /// </summary>
    /// <returns>二级指标列表</returns>
    public DataTable GetGuide2List()
    {
        string strSQL = "SELECT *,(SELECT GuideName FROM Sys_Guide WHERE GuideID = Sys_Guide2.GuideID) AS GuideName FROM Sys_Guide2 Order By Guide2ID Desc";

        return CMMgr.GetDataTable(strSQL);
    }

    /// <summary>
    /// 函数 GetGuide2List | 获取二级指标信息列表
    /// </summary>
    /// <returns>二级指标列表</returns>
    public DataTable GetGuide2List(string GuideID)
    {
        string strSQL = "SELECT *,(SELECT GuideName FROM Sys_Guide WHERE GuideID = Sys_Guide2.GuideID) AS GuideName FROM Sys_Guide2 WHERE GuideID = " + GuideID + " Order By Guide2ID Desc";

        return CMMgr.GetDataTable(strSQL);
    }

    #endregion

}
