using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// 专业管理类
/// </summary>
public class SpecMgr
{
	public SpecMgr()
	{
        
	}

    #region　函数UpdateSpec | 更新Spec信息
    /// <summary>
    /// 更新Spec类信息
    /// </summary>
    /// <param name="spec">专业管理类参数</param>
    /// <returns>更新成功返回true 否则返回false</returns>
    public bool UpdateSpec(Spec spec)
    {
        string strSQL = "";
        SqlDataAdapter sa = null;
        SqlCommandBuilder builder = null;
        DataRow row = null;
        DataTable dt = null;
        bool returnValue = false;
        if (spec != null)
        {
            if (spec.SpecID == 0)
            {
                strSQL = "SELECT Top 0 * FROM Sys_Spec";
            }
            else
            {
                strSQL = "SELECT Top 1 * FROM Sys_Spec WHERE SpecID = " + spec.SpecID.ToString();
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

            row["SpecName"] = spec.SpecName;
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(row);
            }

            SqlConnection conn = CMMgr.GetConnection();
            using (sa = new SqlDataAdapter("SELECT Top 0 * FROM Sys_Spec", conn))
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

    #region　函数GetSpec | 获取Spec信息
    /// <summary>
    /// 获取一条Spec类信息
    /// </summary>
    /// <param name="SpecID">登录编号</param>
    /// <returns>一条Spec类记录</returns>
    public Spec GetSpec(string SpecID)
    {
        Spec spec = new Spec();

        string strSQL = "SELECT Top 1 * FROM Sys_Spec WHERE SpecID = " + SpecID;
        DataTable dt = CMMgr.GetDataTable(strSQL);
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            spec.SpecID = int.Parse(row["SpecID"].ToString());
            spec.SpecName = row["SpecName"].ToString();
            return spec;
        }
        else
        {
            return spec;
        }
    }
    #endregion

    #region　函数DelSpec | 删除Spec信息
    /// <summary>
    /// 删除Spec类信息
    /// </summary>
    /// <param name="SpecID">登录编号参数</param>
    public void DelSpec(string SpecID)
    {
        string strSQL = "DELETE FROM Sys_Spec WHERE SpecID = " + SpecID;
        CMMgr.ExecuteNonQuery(strSQL);
    }

    public void DelSpec(int SpecID)
    {
        this.DelSpec(SpecID.ToString());
    }
    #endregion

    #region 函数 GetSpecList | 获取专业信息列表
    /// <summary>
    /// 函数 GetSpecList | 获取专业信息列表
    /// </summary>
    /// <param name="SpecID">登录名称</param>
    /// <param name="SpecName">真实名称</param>
    /// <returns>专业列表</returns>
    public DataTable GetSpecList()
    {
        string strSQL = "SELECT * FROM Sys_Spec Order By SpecID Desc";

        return CMMgr.GetDataTable(strSQL);
    }

    #endregion

}
