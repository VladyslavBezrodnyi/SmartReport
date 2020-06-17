import React from 'react'
import { connect } from 'react-redux'
import { FormattedMessage } from 'react-intl'

class Home extends React.PureComponent<any>{
    render() {
        return (
            <div>
                <p><FormattedMessage id="home.hello" defaultMessage="Error!"/></p>
            </div>
        )
    }
}

const HomeContainer = connect()(Home)
export default HomeContainer;