import React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'
import { loadLocales } from '../../redux/actions/locales-actions'
import { SwitcherProps } from '../../types/component-types'
import { Select } from 'antd'

const { Option } = Select;

class Switcher extends React.PureComponent<SwitcherProps>{
  state = {
    value: 'en',
    locales: Object.keys(this.props.locales)
  }

  handleChange = (value: string) => {
    this.setState({
      value
    });
    this.props.loadLocales(value)
  }

  render() {
    return (
      <Select defaultValue={this.state.value}
        value={this.state.value}
        style={{ width: 120 }}
        onChange={this.handleChange}>
        {this.state.locales.map((locale: string, i: number) => {
          return (<Option value={locale} key={i}>{locale}</Option>);
        })}
      </Select>
    )
  }
}

const mapStateToProps = (state: any) => {
  return {
    currentLocale: state.intl.locale,
    locales: state.locales,
  }
};

const mapDispatchToProps = (dispatch: any) => {
  return bindActionCreators({
    loadLocales: loadLocales
  }, dispatch)
}

const SwitchLocale = connect(mapStateToProps, mapDispatchToProps)(Switcher);
export default SwitchLocale;