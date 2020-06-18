import React from 'react';
import { BrowserRouter, Route, Switch, Link } from 'react-router-dom';
import './App.css';
import { Layout, Menu } from 'antd';
import "antd/dist/antd.min.css";
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import HeaderContainer from '../Header/Header';
import { IntlProvider } from 'react-intl-redux'
import HomeContainer from '../Home/Home';
import { AppProps } from '../../types/props-types';
import { loadDefaultLocale } from '../../redux/actions/locales-actions'
import {
  MenuUnfoldOutlined,
  MenuFoldOutlined,
  UserOutlined,
  VideoCameraOutlined,
  UploadOutlined,
  FileOutlined
} from '@ant-design/icons'
import { FormattedMessage } from 'react-intl';

const { Header, Sider, Content, Footer } = Layout;


class App extends React.PureComponent<AppProps>{
  state = {
    collapsed: false,
  };
  constructor(props: AppProps) {
    super(props);
    this.props.loadDefaultLocale();
  }

  onCollapse = (collapsed: any) => {
    console.log(collapsed);
    this.setState({ collapsed });
  };


  render() {
    return (
      <BrowserRouter>
        <IntlProvider locale={this.props.locale} messages={this.props.messages}>
          <Layout>
            <Sider collapsible collapsed={this.state.collapsed} onCollapse={this.onCollapse}>
              <div className="logo"></div>
              <Menu theme="dark" mode="inline" defaultSelectedKeys={['1']}>
                <Menu.Item key="1" icon={<UserOutlined />}>
                  <Link to="/"><FormattedMessage id="header.home" defaultMessage="Error!" /></Link>
                </Menu.Item>
                <Menu.Item key="2" icon={<VideoCameraOutlined />}>
                  nav 2
                </Menu.Item>
                <Menu.Item key="3" icon={<UploadOutlined />}>
                  nav 3
                </Menu.Item>
                <Menu.Item key="9" icon={<FileOutlined />} />
              </Menu>
            </Sider>
            <Layout className="site-layout">
              <HeaderContainer>
              </HeaderContainer>
              <Content className="site-layout-background"
                style={{
                  margin: '24px 16px',
                  padding: 24,
                  minHeight: '100%'
                }}>
                <div className="site-layout-background" style={{ padding: 24, minHeight: '100%' }}>
                  <Switch>
                    <Route exact path="/" component={HomeContainer} />
                  </Switch>
                </div>
              </Content>
              <Footer style={{ textAlign: 'center' }}>{new Date().getFullYear()}</Footer>
            </Layout>
          </Layout>
        </IntlProvider>
      </BrowserRouter>
    )
  }
}

const mapStateToProps = (state: any) => {
  return {
    locale: state.intl.locale,
    messages: state.intl.messages
  }
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators({
    loadDefaultLocale: loadDefaultLocale,
  }, dispatch)
}

const AppContainer = connect(mapStateToProps, mapDispatchToProps)(App);
export default AppContainer;
