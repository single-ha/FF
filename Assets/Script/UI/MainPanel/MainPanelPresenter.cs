namespace Assets.Script.UI.MainPanel
{
    public class MainPanelPresenter:PanelPresenterBase
    {
        protected override void ConfigPanelPresenter(PanelConfig panelConfig)
        {
            panelConfig.panelType = PanelType.Normal;
        }
    }
}