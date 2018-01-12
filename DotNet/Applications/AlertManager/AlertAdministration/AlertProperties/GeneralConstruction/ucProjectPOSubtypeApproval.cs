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
    public partial class ucProjectPOSubtypeApproval : ucPropertyInterface
    {
        private CommonComponents.ucStakeholderType st;

        public ucProjectPOSubtypeApproval()
        {
            InitializeComponent();
        }

        private void ucProjectPOSubtypeApproval_Load(object sender, EventArgs e)
        {
            st = new AlertProperties.CommonComponents.ucStakeholderType(Connection, DevXMgr, AlertPointID);
            st.Dock = DockStyle.Fill;
            st.Parent = panelControl1;
            st.BringToFront();
        }
    }
}
