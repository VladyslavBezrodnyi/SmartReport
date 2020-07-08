import React from 'react';
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { Divider, Button, Modal, Radio, Space, Input, Checkbox, message } from 'antd';
import { FormattedMessage } from 'react-intl';
import { List } from 'antd';
import Text from 'antd/lib/typography/Text';
import { UserTasksListProps } from '../../types/props-types';
import { TaskDTO, ReportDTO } from '../../types/DTO-types';
import { getUserTasks } from '../../redux/actions/task-actions';
import { TaskCreationProps } from '../../types/props-types';
import { createTask } from '../../redux/actions/task-actions';
import { createReport } from '../../redux/actions/report-actions';
import InfiniteScroll from 'react-infinite-scroller'

let initState = () => {
    return {
        visible: false,
        report: {
            reportText: "",
            date: new Date(),
            tasks: new Array<TaskDTO>(),
        } as ReportDTO
    }
}

class UserTasksList extends React.PureComponent<UserTasksListProps>{
    state = initState();

    constructor(props: UserTasksListProps) {
        super(props);
        this.props.getUserTasksList();
    }

    showModal = () => {
        this.setState({
            visible: true,
        });
    };

    handleCancel = (e: any) => {
        this.setState(initState());
    };

    onChangeReportText = (e: any) => {
        console.log(e.target.value)
        this.setState({
            report: {
                ...this.state.report,
                reportText: e.target.value,
            }
        });
        console.log(this.state)
    }

    onChooseTasks = (checkedValues: any) => {
        console.log('checked = ', checkedValues);
        let checkedTasks = Array<TaskDTO>();
        checkedValues.map((item: string, i: number) => {
            checkedTasks.push({
                id: Number(item)
            } as TaskDTO)
        });
        this.setState({
            report: {
                ...this.state.report,
                tasks: checkedTasks as Array<TaskDTO>,
            }
        })
        console.log(this.state.report.tasks)
    }

    onOk = () => {
        if (this.state.report.tasks.length != 0) {
            this.props.createReport({ ...this.state.report, data: new Date() } as ReportDTO)
                .finally(() => {
                    this.props.getUserTasksList()
                });
            this.setState({ ...initState() });
        } else {
            message.warning("Choose tasks!")
        }
    }

    render() {
        return (
            <div>
                <Button type="primary" onClick={this.showModal}>
                    <FormattedMessage id="user.createReport" defaultMessage="Error!" />
                </Button>
                <Modal
                    title={<FormattedMessage id="user.createReport" defaultMessage="Error!" />}
                    visible={this.state.visible}
                    onCancel={this.handleCancel}
                    onOk={this.onOk}
                >

                    <Divider><FormattedMessage id="user.reportDescription" defaultMessage="Error!" /></Divider>
                    <Input.TextArea value={this.state.report.reportText}
                        onChange={this.onChangeReportText}></Input.TextArea>
                    <Divider><FormattedMessage id="user.tasks" defaultMessage="Error!" /></Divider>
                    <div className="demo-infinite-container">
                        <InfiniteScroll
                            initialLoad={false}
                            pageStart={0}
                            loadMore={() => { }}
                            useWindow={false}
                        >
                            <Checkbox.Group
                                onChange={this.onChooseTasks}>
                                <Space direction="vertical">
                                    {this.props.userTasksList.map((item: TaskDTO, i: number) => {
                                        return (
                                            <Checkbox key={item.id} value={item.id}>
                                                {<Text strong>{`${item.id} - ${item.name}`}</Text>}
                                            </Checkbox >
                                        );
                                    })}
                                </Space>
                            </Checkbox.Group>
                        </InfiniteScroll>
                    </div>
                </Modal>
                <Divider><FormattedMessage id="user.tasks" defaultMessage="Error!" /></Divider>
                <List
                    itemLayout="horizontal"
                    dataSource={this.props.userTasksList}
                    renderItem={(item: TaskDTO, i: number) => (
                        <List.Item>
                            <List.Item.Meta
                                title={<Text strong>{`${item.id} - ${item.name}`}</Text>}
                                description={
                                    <>
                                        <Text><FormattedMessage id="description" defaultMessage="Error!" />{": "}{item.description}</Text>
                                    </>}
                            />
                        </List.Item>
                    )}
                />
            </div>
        )
    }

}

const mapStateToProps = (state: any) => {
    return {
        userTasksList: state.task.userTasksList
    }
};

const mapDispatchToProps = (dispatch: any) => {
    return bindActionCreators({
        getUserTasksList: getUserTasks,
        createReport: createReport
    }, dispatch)
}

const UserTasksListContainer = connect(mapStateToProps, mapDispatchToProps)(UserTasksList);
export default UserTasksListContainer;