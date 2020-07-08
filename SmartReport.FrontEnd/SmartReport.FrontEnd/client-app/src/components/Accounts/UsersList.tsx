import React from 'react';
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { UsersListProps } from '../../types/props-types';
import { FormattedMessage } from 'react-intl';
import { UserDTO, UserTaskDTO } from '../../types/DTO-types';
import Text from 'antd/lib/typography/Text';
import { getUsers } from '../../redux/actions/account-actions';
import AccountCreationContainer from './AccountCreation';
import { Divider, List, message, Avatar, Spin, Button, Modal, Radio, Space } from 'antd';
import { getTasks, assignTask } from '../../redux/actions/task-actions';
import InfiniteScroll from 'react-infinite-scroller'
import { TaskDTO } from '../../types/DTO-types';
import './TaskAssigning.css';

class UsersList extends React.PureComponent<UsersListProps, any>{
    state = {
        assignVisible: false,
        userTask: {
            userId: '',
            taskId: -1
        } as UserTaskDTO
    };

    constructor(props: UsersListProps) {
        super(props);
        this.props.getUsers();
        this.props.getTasksList();
    }

    onAssign = (id: string) => {
        console.log(id)
        this.setState({
            userTask: {
                ...this.state.userTask,
                userId: id
            },
            assignVisible: true,
        })
    }

    onChooseTask = (e: any) => {
        console.log(e.target.value);
        this.setState({
            userTask: {
                ...this.state.userTask,
                taskId: e.target.value
            }
        })
    }

    onHandleTaskAssign = (e: any) => {
        if (this.state.userTask.taskId != -1 && this.state.userTask.userId != '') {
            this.props.assignTask(this.state.userTask);
            this.setState({
                userTask: {
                    taskId: -1,
                    userId: ''
                },
                assignVisible: false,
            })
        } else {
            message.warning("Choose task!");
        }
    }

    handleCancel = (e: any) => {
        console.log(e);
        this.setState({
            userTask: {
                taskId: -1,
                userId: ''
            },
            assignVisible: false,
        });
    };

    render() {
        return (
            <div>
                <AccountCreationContainer />
                <Divider><FormattedMessage id="admin.accounts" defaultMessage="Error!" /></Divider>
                <List
                    itemLayout="horizontal"
                    dataSource={this.props.usersList}
                    renderItem={(item: UserDTO, i: number) => (
                        <List.Item
                            actions={[
                                <Button key="i"
                                    onClick={() => { this.onAssign(item.id) }}
                                >
                                    <FormattedMessage id="admin.accounts.assignTask" defaultMessage="Error!" />
                                </Button>]}
                        >
                            <List.Item.Meta
                                title={<Text strong>{`${i + 1} - ${item.name}`}</Text>}
                                description={<div>
                                    <p>{`Email: ${item.userName}`}</p>
                                </div>}
                            />
                        </List.Item>
                    )}
                />
                <div>
                    <Modal
                        title={<FormattedMessage id="admin.accounts.assignTask" defaultMessage="Error!" />}
                        visible={this.state.assignVisible}
                        onCancel={this.handleCancel}
                        onOk={this.onHandleTaskAssign}
                    >
                        <Divider><FormattedMessage id="admin.accounts.tasks" defaultMessage="Error!" /></Divider>
                        <div className="demo-infinite-container">
                            <InfiniteScroll
                                initialLoad={false}
                                pageStart={0}
                                loadMore={() => { }}
                                useWindow={false}
                            >
                                <Radio.Group buttonStyle='outline'
                                    onChange={this.onChooseTask}>
                                    <Space direction="vertical">
                                        {this.props.tasksList.map((item: TaskDTO, i: number) => {
                                            return (
                                                <Radio key={item.id} value={item.id}>
                                                    {<Text strong>{`${item.id} - ${item.name}`}</Text>}
                                                </Radio>
                                            );
                                        })}
                                    </Space>
                                </Radio.Group>
                            </InfiniteScroll>
                        </div>
                    </Modal>
                </div>
            </div>
        )
    }

}

const mapStateToProps = (state: any) => {
    return {
        tasksList: state.task.tasksList,
        usersList: state.acc.usersList,
    }
};

const mapDispatchToProps = (dispatch: any) => {
    return bindActionCreators({
        getUsers: getUsers,
        getTasksList: getTasks,
        assignTask: assignTask
    }, dispatch)
}

const UsersListContainer = connect(mapStateToProps, mapDispatchToProps)(UsersList);
export default UsersListContainer;
