using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace AlertViews.FixedAssets
{
    public partial class avFAPreventativeMaintenance : AlertManager.AlertView
    {
        public avFAPreventativeMaintenance()
        {
            InitializeComponent();
        }

        public override void LoadAlert(int AlertID, string Subject, string Module, DateTime Received, string Notes, int DetailID)
        {
            base.LoadAlert(AlertID, Subject, Module, Received, Notes, DetailID);

        }
    }
}
