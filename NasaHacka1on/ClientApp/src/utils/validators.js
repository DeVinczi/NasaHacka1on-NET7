export const isNotEmpty = (value) => value.trim().length > 0;

export const isMinFiveChars = (value) => value.trim().length >= 5;

export const isMinNineChars = (value) => value.trim().length >= 9;

export const isEmail = (value) =>
  /^[a-z0-9][a-z0-9-_\.]+@([a-z]|[a-z0-9]?[a-z0-9-]+[a-z0-9])\.[a-z0-9]{2,10}(?:\.[a-z]{2,10})?$/.test(
    value.trim()
  );
