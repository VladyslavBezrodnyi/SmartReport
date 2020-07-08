import React from 'react';
import { BrowserRouter, Route, Switch, Link } from 'react-router-dom';
import './App.css';
import { Layout, Menu, Space, Typography, Button } from 'antd';
import "../../../node_modules/antd/dist/antd.min.css";
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
  FileOutlined,
  TeamOutlined,
  CheckSquareOutlined
} from '@ant-design/icons'
import { FormattedMessage } from 'react-intl';
import UsersListContainer from '../Accounts/UsersList';
import TasksListContainer from '../Tasks/TasksList';
import UserTasksListContainer from '../Tasks/UserTasksList';

const { Header, Sider, Content, Footer } = Layout;
const { Text } = Typography;

class App extends React.PureComponent<AppProps>{
  state = {
    collapsed: true,
    menuItem: "1",
    current: "1",
  };

  constructor(props: AppProps) {
    super(props);
    this.props.loadDefaultLocale();
  }

  onCollapse = (collapsed: any) => {
    console.log(collapsed);
    this.setState({ collapsed });
  };

  getUserInfo = () => {
    return (
      <Space direction="vertical"
        align="center"
        style={{ width: "100%", color: "white" }}>
        <Text style={{ color: "white" }}>
          {this.props.auth?.user?.userName}
        </Text>
        <Text style={{ color: "white" }}>
          {`(${this.props.auth?.user?.roles})`}
        </Text>
      </Space>
    );
  }

  db = () => {

  }

  onClickMenuItem = (e: any) => {
    this.setState({
      menuItem: e.key
    })
  }

  getMenuItems = () => {
    if (this.props.auth.isAuthenticated) {
      if (this.props.auth.user?.roles == "admin") {
        return (
          <Menu theme="dark"
            mode="inline"
            defaultSelectedKeys={[this.state.menuItem]}
            onClick={this.onClickMenuItem}>
            <Menu.Item key="1" icon={<UserOutlined />}>
              <Link to="/"><FormattedMessage id="header.home" defaultMessage="Error!" /></Link>
            </Menu.Item>
            <Menu.Item key="2" icon={<TeamOutlined />}>
              <Link to="/accounts"><FormattedMessage id="header.accounts" defaultMessage="Error!" /></Link>
            </Menu.Item>
            <Menu.Item key="3" icon={<CheckSquareOutlined />}>
              <Link to="/tasks"><FormattedMessage id="header.tasks" defaultMessage="Error!" /></Link>
            </Menu.Item>
          </Menu>
        )
      } else if (this.props.auth.user?.roles == "user") {
        return (
          <Menu theme="dark"
            mode="inline"
            defaultSelectedKeys={[this.state.menuItem]}
            onClick={this.onClickMenuItem}>
            <Menu.Item key="1" icon={<UserOutlined />}>
              <Link to="/"><FormattedMessage id="header.home" defaultMessage="Error!" /></Link>
            </Menu.Item>
            <Menu.Item key="4" icon={<CheckSquareOutlined />}>
              <Link to="/my-tasks"><FormattedMessage id="header.tasks" defaultMessage="Error!" /></Link>
            </Menu.Item>
          </Menu>
        )
      }
    }
  }

  handleClickMobileMenu = (e: any) => {
    console.log('click ', e);
    this.setState({ current: e.key });
  }

  getMenuSider = () => {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent) && this.props.auth.isAuthenticated && this.props.auth.user?.roles == "user") {
      return (
        <Menu onClick={this.handleClickMobileMenu} selectedKeys={[this.state.current]} mode="horizontal">
          <Menu.Item key="1" icon={<UserOutlined />}>
            <Link to="/"><FormattedMessage id="header.home" defaultMessage="Error!" /></Link>
          </Menu.Item>
          <Menu.Item key="4" icon={<CheckSquareOutlined />}>
            <Link to="/my-tasks"><FormattedMessage id="header.tasks" defaultMessage="Error!" /></Link>
          </Menu.Item>
        </Menu>
      );
    } else {
      return (
        <Sider collapsible collapsed={this.state.collapsed} onCollapse={this.onCollapse}>
          {(!this.state.collapsed && this.props.auth.isAuthenticated) ? (this.getUserInfo()) : (<div></div>)}
          {this.getMenuItems()}
        </Sider>
      );
    }
  }

  render() {
    return (
      <BrowserRouter>
        <IntlProvider locale={this.props.locale} messages={this.props.messages}>
          <Layout style={{ minHeight: '100vh' }}>
            {(this.props.auth.isAuthenticated) ? (
              this.getMenuSider()
            ) : (
                <></>
              )}
            <Layout className="site-layout">
              <HeaderContainer />
              <Content className="site-layout-background"
                style={{
                  margin: '24px 16px',
                  padding: 24,
                  minHeight: '60%'
                }}>
                <div className="site-layout-background" style={{ padding: 24, }}>
                  <Switch>
                    <Route exact path="/" component={HomeContainer} />
                    <Route exact path="/accounts" component={UsersListContainer} />
                    <Route exact path="/tasks" component={TasksListContainer} />
                    <Route exact path="/my-tasks" component={UserTasksListContainer} />
                  </Switch>
                </div>
              </Content>
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
    messages: state.intl.messages,
    auth: state.auth
  }
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators({
    loadDefaultLocale: loadDefaultLocale,
  }, dispatch)
}

const AppContainer = connect(mapStateToProps, mapDispatchToProps)(App);
export default AppContainer;
