import React from "react";

interface ILoginProps {
  portalSettings: IPortalSettings;
  buildInfo: IBuildInfo;
  providers: ProvidersType;
  capabilities: ICapabilities;
}
const App: React.FC<ILoginProps> = (props) => {
  return <div>Test: {JSON.stringify(props)}</div>;
};

export default App;
