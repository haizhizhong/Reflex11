using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertProperties.GeneralConstruction
{
    public partial class ucProjectBudActNotifications : ucPropertyInterface
    {
        private CommonComponents.ucStakeholderType st;
        private bool isLoading = false;

        public ucProjectBudActNotifications()
        {
            InitializeComponent();            
        }

        private void ucProjectBudActNotifications_Load(object sender, EventArgs e)
        {
            st = new AlertProperties.CommonComponents.ucStakeholderType(Connection, DevXMgr, AlertPointID);
            st.Dock = DockStyle.Fill;
            st.Parent = panelControl1;
            st.BringToFront();
            isLoading = true;
            string sSelect = "select isnull(Proj_BudAct_Threshold_Pct,10) from Alert_Properties_Setup";
            teThresholdPct.EditValue = Connection.SQLExecutor.ExecuteScalar(sSelect, Connection.TRConnection);
            isLoading = false;
        }

        private void teThresholdPct_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoading)
                return;
            int threshold = -1;
            try
            {
                threshold = Convert.ToInt32(teThresholdPct.EditValue);
            }
            catch
            {
            }
            if (threshold >= 0 && threshold < 100)
            {
                string sUpdate = "update Alert_Properties_Setup set Proj_BudAct_Threshold_Pct =" + teThresholdPct.EditValue;
                Connection.SQLExecutor.ExecuteNonQuery(sUpdate, Connection.TRConnection);
            }
            else
                Popup.ShowPopup("Threshold must be between 0 and 99.");
        }
    }
}
