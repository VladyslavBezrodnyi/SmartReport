import React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'
import { loadLocales } from '../../redux/actions/locales-actions'
import { SwitcherProps } from '../../types/component-types'
import { Select } from 'antd'
import { Radio } from 'antd';

const { Option } = Select;

class Switcher extends React.PureComponent<SwitcherProps>{
  handleChange = (e: any) => {
    this.props.loadLocales(e.target.value)
  }

  render() {
    return (
      <Radio.Group defaultValue={this.props.currentLocale}
        buttonStyle="solid"
        onChange={this.handleChange}>
        {Object.keys(this.props.locales).map((locale: string, i: number) => {
          return (<Radio.Button value={locale} key={i}>{locale}</Radio.Button>);
        })}
      </Radio.Group>
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