window.getInnerHtml = (elementId) => {
    var element = document.getElementById(elementId);
    if (element) {
        return element.innerHTML;
    }
    return null;
};