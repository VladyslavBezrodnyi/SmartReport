import { updateIntl } from 'react-intl-redux';
import { UPDATE_LOCALES } from './../../constants/intl-constants';

export const loadDefaultLocale = () => (despatch: any) => {
    const locales = require("../../locales.json");
    despatch(
        updateIntl({
            locale: "en",
            messages: locales["en"],
        }));
};

export const loadLocales = (locale: string) => (despatch: any) => {
    const locales = require("../../locales.json");
    despatch(
        updateIntl({
            locale,
            messages: locales[locale],
        }));
};

export const updateLocales = () => (despatch: any) => {
    const locales = require("../../locales.json");
    despatch({
        type: UPDATE_LOCALES,
        payload: {
            ...locales
        }
    });
};