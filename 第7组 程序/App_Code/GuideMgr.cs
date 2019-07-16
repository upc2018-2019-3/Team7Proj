using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// 一级指标管理类
/// </summary>
public class GuideMgr
{
	public GuideMgr()
	{
        
	}

    #region　函数UpdateGuide | 更新Guide信息
    /// <summary>
    /// 更新Guide类信息
    /// </summary>
    /// <param name="guide">一级指标管理类参数</param>
    /// <returns>更新成功返回true 否则返回false</returns>
    public bool UpdateGuide(Guide guide)
    {
        string strSQL = "";
        SqlDataAdapter sa = null;
        SqlCommandBuilder builder = null;
        DataRow row = null;
        DataTable dt = null;
        bool returnValue = false;
        if (guide != null)
        {
            if (guide.GuideID == 0)
            {
                strSQL = "SELECT Top 0 * FROM Sys_Guide";
            }
            else
            {
                strSQL = "SELECT Top 1 * FROM Sys_Guide WHERE GuideID = " + guide.GuideID.ToString();
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

            row["GuideCode"] = guide.GuideCode;
            row["GuideName"] = guide.GuideName;
            row["Scale"] = guide.Scale;
            row["Scale1"] = guide.Scale1;
            row["Scale2"] = guide.Scale2;
            row["Scale3"] = guide.Scale3;
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(row);
            }

            SqlConnection conn = CMMgr.GetConnection();
            using (sa = new SqlDataAdapter("SELECT Top 0 * FROM Sys_Guide", conn))
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

    #region　函数GetGuide | 获取Guide信息
    /// <summary>
    /// 获取一条Guide类信息
    /// </summary>
    /// <param name="GuideID">登录编号</param>
    /// <returns>一条Guide类记录</returns>
    public Guide GetGuide(string GuideID)
    {
        Guide guide = new Guide();

        string strSQL = "SELECT Top 1 * FROM Sys_Guide WHERE GuideID = " + GuideID;
        DataTable dt = CMMgr.GetDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            guide.GuideID = int.Parse(row["GuideID"].ToString());
            guide.GuideCode = row["GuideCode"].ToString();
            guide.GuideName = row["GuideName"].ToString();
            guide.Scale = int.Parse(row["Scale"].ToString());
            guide.Scale1 = int.Parse(row["Scale1"].ToString());
            guide.Scale2 = int.Parse(row["Scale2"].ToString());
            guide.Scale3 = int.Parse(row["Scale3"].ToString());
            return guide;
        }
        else
        {
            return guide;
        }
    }
    #endregion

    #region　函数DelGuide | 删除Guide信息
    /// <summary>
    /// 删除Guide类信息
    /// </summary>
    /// <param name="GuideID">登录编号参数</param>
    public void DelGuide(string GuideID)
    {
        string strSQL = "DELETE FROM Sys_Guide WHERE GuideID = " + GuideID;
        CMMgr.ExecuteNonQuery(strSQL);
    }

    public void DelGuide(int GuideID)
    {
        this.DelGuide(GuideID.ToString());
    }
    #endregion

    #region 函数 GetGuideList | 获取一级指标信息列表
    /// <summary>
    /// 函数 GetGuideList | 获取一级指标信息列表
    /// </summary>
    /// <param name="GuideID">登录名称</param>
    /// <param name="GuideName">真实名称</param>
    /// <returns>一级指标列表</returns>
    public DataTable GetGuideList()
    {
        string strSQL = "SELECT * FROM Sys_Guide Order By GuideID Desc";

        return CMMgr.GetDataTable(strSQL);
    }

    #endregion

}
