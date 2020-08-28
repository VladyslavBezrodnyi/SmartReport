import { GET_REPORT_BY_USER_ID } from './../../constants/report-constants';
import { ReportDTO } from './../../types/DTO-types';
import { ReportState } from './../../types/state-types';
import { Action } from './../../types/common-types';

const initialState = {
    userReportsList: Array<ReportDTO>()
} as any

const reportReducer = (state: ReportState = initialState, action: Action): ReportState => {
    switch (action.type) {
        case GET_REPORT_BY_USER_ID:
            return {
                ...state,
                userReportsList: action.payload?.userReportsList,
            }
        default:
            return {
                ...state
            }
    }
}
export default reportReducer;