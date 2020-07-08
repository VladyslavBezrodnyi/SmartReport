import React from 'react'
import { connect } from 'react-redux'
import { FormattedMessage } from 'react-intl'
import Text from 'antd/lib/typography/Text'
import { Space, Divider, Alert } from 'antd'
import { bindActionCreators } from 'redux'
import { HomeProps } from '../../types/props-types'
import { Typography } from 'antd';

const { Title } = Typography;

class Home extends React.PureComponent<HomeProps>{
    getHello = (hello: any) => {
        if (this.props.auth?.isAuthenticated) {
            return (
                <>
                    {hello}{', '} <Text underline>{this.props.auth?.user?.userName}</Text>{'!'}
                </>
            );
        } else {
            return (
                <>
                    {hello}{`!`}
                </>
            );
        }
    }

    getStatus = () => {
        if (this.props.auth?.isAuthenticated && this.props.auth?.user?.roles == 'user') {
            if (this.props.auth?.work != null || this.props.auth?.work != undefined) {
                if (this.props.auth?.work?.isWork) {
                    return (
                        <Alert message={<FormattedMessage id="user.work" defaultMessage="Error!" />} type="success" />
                    );
                } else {
                    return (
                        <Alert message={<FormattedMessage id="user.notWork" defaultMessage="Error!" />} type="warning" />
                    );
                }
            }
        }
    }

    render() {
        return (
            <>

                <Divider>{this.getHello(<FormattedMessage id="home.hello" defaultMessage="Error!" />)}</Divider>
                {this.getStatus()}

            </>
        )
    }
}


const mapStateToProps = (state: any) => {
    return {
        auth: state.auth
    }
};

const mapDispatchToProps = (dispatch: any) => {
    return bindActionCreators({
    }, dispatch)
}

const HomeContainer = connect(mapStateToProps, mapDispatchToProps)(Home)
export default HomeContainer;