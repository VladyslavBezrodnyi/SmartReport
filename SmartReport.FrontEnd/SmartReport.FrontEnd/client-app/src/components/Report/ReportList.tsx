import React from 'react';
import { bindActionCreators } from 'redux'
import { connect } from 'react-redux'
import { ReportListProps } from '../../types/props-types';
import { FormattedMessage } from 'react-intl';
import { ReportDTO, TaskDTO } from '../../types/DTO-types';
import Text from 'antd/lib/typography/Text';
import { Divider, List, message, Avatar, Spin, Button, Modal, Radio, Space } from 'antd';
import { getReportByUserId } from '../../redux/actions/report-actions';
import InfiniteScroll from 'react-infinite-scroller';
import { Link, withRouter } from 'react-router-dom';

class ReportList extends React.PureComponent<ReportListProps, any>{
    state = {
        assignVisible: false,
        reportId: null
    };

    constructor(props: ReportListProps) {
        super(props);
        console.log("ReportList",this.props.user.id);
        this.props.getReportByUserId(this.props.user.id);
        console.log(this.props.userReportsList);
    }

    handleCancel = (e: any) => {
        console.log(e);
        this.setState({
            assignVisible: false,
            reportId: null
        });
    };

    onDetail = (reportId?: number) => {
        console.log(reportId);
        this.setState({
            assignVisible: true,
            reportId: reportId
        });
    };

    render() {
        return (
            <div>
                <Divider><FormattedMessage id="user.myReports" defaultMessage="Error!" /></Divider>
                <List
                    itemLayout="horizontal"
                    dataSource={this.props.userReportsList}
                    renderItem={(item: ReportDTO, i: number) => (
                        <List.Item
                            actions={[
                                <Button key="i"
                                    onClick={() => { this.onDetail(item.id) }}
                                >
                                    <FormattedMessage id="admin.accounts.reports.viewTasks" defaultMessage="Error!" />
                                </Button>]}
                        >
                            <List.Item.Meta title={<Text strong>{`${i + 1} - ${item.reportText}`}</Text>} />
                        </List.Item>
                    )}
                />
                <div>
                    <Modal
                        title={<FormattedMessage id="admin.accounts.reports.tasks" defaultMessage="Error!" />}
                        visible={this.state.assignVisible}
                        onCancel={this.handleCancel}
                        footer={null}
                    >
                        <div className="demo-infinite-container">
                            <InfiniteScroll
                                initialLoad={false}
                                pageStart={0}
                                loadMore={() => { }}
                                useWindow={false}
                            >
                                <List
                                    itemLayout="horizontal"
                                    dataSource={this.props.userReportsList.find(r => r.id === this.state.reportId)?.tasks as TaskDTO[]}
                                    renderItem={(item: TaskDTO, i: number) => (
                                        <List.Item>
                                            <List.Item.Meta
                                                title={<Text strong>{`${item.id} - ${item.name}`}</Text>}
                                                description={
                                                    <>
                                                        <Text><FormattedMessage id="description" defaultMessage="Error!" />{": "}{item.description}</Text>
                                                    </>
                                                }
                                            />
                                        </List.Item>
                                    )}
                                />
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
        userReportsList: state.report.userReportsList,
        user: state.auth.user
    }
};

const mapDispatchToProps = (dispatch: any) => {
    return bindActionCreators({
        getReportByUserId: getReportByUserId,
    }, dispatch)
}

const ReportListContainer = connect(mapStateToProps, mapDispatchToProps)(ReportList as any);
export default ReportListContainer;
