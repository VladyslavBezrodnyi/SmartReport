import React from 'react';
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { AccountCreationProps } from '../../types/props-types';
import { register } from '../../redux/actions/auth-actions';
import { Form, Input, Button } from 'antd';
import { FormattedMessage } from 'react-intl';
import { RegistrationDTO } from '../../types/DTO-types';

const layout = {
    labelCol: { span: 8 },
    wrapperCol: { span: 16 },
};
const tailLayout = {
    wrapperCol: { offset: 8, span: 16 },
};

class AccountCreation extends React.PureComponent<AccountCreationProps>{
    onFinish = (values: any) => {
        console.log('Success:', values);
        this.props.register(values as RegistrationDTO);
    };

    onFinishFailed = (errorInfo: any) => {
        console.log('Failed:', errorInfo);
    };

    render() {
        return (
            <Form
                {...layout}
                name="basic"
                initialValues={{ remember: true }}
                onFinish={this.onFinish}
                onFinishFailed={this.onFinishFailed}
            >
                <Form.Item
                    label={<FormattedMessage id="name" defaultMessage="Error!" />}
                    name="name"
                    rules={[{ required: true, message: 'Please input your name!' }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label={<FormattedMessage id="email" defaultMessage="Error!" />}
                    name="email"
                    rules={[{ required: true, message: 'Please input your email!' }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label={<FormattedMessage id="password" defaultMessage="Error!" />}
                    name="password"
                    rules={[{ required: true, message: 'Please input your password!' }]}
                >
                    <Input.Password />
                </Form.Item>

                <Form.Item {...tailLayout}>
                    <Button type="primary" htmlType="submit">
                        {<FormattedMessage id="submit" defaultMessage="Error!" />}
                    </Button>
                </Form.Item>
            </Form >
        );
    }
}

const mapStateToProps = (state: any) => {
    return {
    }
};

const mapDispatchToProps = (dispatch: any) => {
    return bindActionCreators({
        register: register
    }, dispatch)
}

const AccountCreationContainer = connect(mapStateToProps, mapDispatchToProps)(AccountCreation);
export default AccountCreationContainer;
