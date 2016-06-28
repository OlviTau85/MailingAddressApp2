/* Model for simple REST API */
function MailAdsListModel($http) {
    var list = {};
    list.getMailAddressesList = function (number) {
        return $http.get('/Data');
        //return $http.get('/Data/GetMailAddressList/' + number);
    };
    /*list.getMailAddressesCount = function () {
        return $http.get('/Data/GetMailAddressesCount');
    };*/
    return list;
};