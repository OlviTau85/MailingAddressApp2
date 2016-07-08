/* Module MailingAddressApp 
 * Author: Udod O.V.
 **/

angular.module("MailingAddressApp").controller('MailAdsListCtrl',
['$scope', 'MailAdsListModel', 'orderByFilter', 'filterFilter', 'translationService', MailAdsListCtrl])
.factory('MailAdsListModel', MailAdsListModel)
.filter('dateRangeFilter', ['dateFilter', dateRangeFilter])
.service('translationService', translationService);

/* Main controller */
function MailAdsListCtrl($scope, MailAdsListModel, orderBy, filter, translationService) {
    
    $scope.mailAddressesList = null;  // list of addresses to show
    $scope.total = 0;                 // total number of records on server in data base

    /* First initialization of page interface */
    MailAdsListModel.init().then(function (data) {      // get initialaze info for page from server
        $scope.total = data.data.count;
        $scope.rangeData.min = $scope.rangeData.valueFrom = data.data.MinHouseNumberFilter;
        $scope.rangeData.max = $scope.rangeData.valueUntil = data.data.MaxHouseNumberFilter;
        $scope.creationDateFilterData.minDate = data.data.MinCreationDate.slice(6, -2);
        $scope.creationDateFilterData.maxDate = data.data.MaxCreationDate.slice(6, -2);
        $scope.creationDateFilterData.reset();
        getMailAddressList();                           // and load first data for first page
    }, function (error) {
        alert("Error! Can't init app! Code=" + error.status);   // else alert with error code
    });

    /* Function for get next page with filtered and sorted data */
    getMailAddressList = function () {
        MailAdsListModel.getMailAddressesList($scope.paginationData.currentPage, $scope.paginationData.itemsPerPage, $scope.totalItems, $scope.sortData.sortField, $scope.sortData.reverse,
                                              $scope.countryFilter, $scope.cityFilter, $scope.streetFilter, $scope.rangeData.valueFrom, $scope.rangeData.valueUntil,
                                              $scope.indexFilter, $scope.creationDateFilterData.dateFrom, $scope.creationDateFilterData.dateUntil).then(function (data) {
            // update page info with new conditions
            $scope.mailAddressesList = data.data.MailAddressesList;
            $scope.paginationData.currentPage = data.data.PageInfo.PageNumber;
            $scope.paginationData.itemsPerPage = data.data.PageInfo.PageSize;
            $scope.paginationData.totalItems = data.data.PageInfo.TotalItems;
            $scope.sortData.sortField = data.data.PageInfo.OrderField;
            $scope.sortData.reverse = data.data.PageInfo.SortReverse;
        }, function (error) {
            alert("Error! Can't get mail address list data! " + error.status);
        });
    };


    /* Localization */
    $scope.translate = function () {
        $scope.selectedLanguage = $scope.radioModel;
        translationService.getTranslation($scope, $scope.selectedLanguage);
    };
    $scope.radioModel = 'ru';  // current language is russian
    $scope.translate();
    
    /* Calls when any filter input changed */
    $scope.onFilterChange = function () {
        getMailAddressList(); // update page data with filtered info
    };

    
    /* Reset all filters */
    $scope.resetFilter = function () {
        $scope.countryFilter = undefined;
        $scope.cityFilter = undefined;
        $scope.streetFilter = undefined;
        $scope.indexFilter = undefined;
        $scope.rangeData.reset();
        $scope.rangeData.onRangeChange();
        $scope.creationDateFilterData.reset();
    };

    /* Date Filter properties */
    $scope.creationDateFilterData = {
        dateFrom: undefined,
        dateUntil: undefined,
        minDate: undefined,
        onDateChange: function () {
            $scope.onFilterChange();
        },
        reset: function () {
            this.dateFrom = this.minDate;
            this.dateUntil = new Date();
        }
    };

    /* Numeric range Filter properties */
    $scope.houseNumberFilterData = {
        houseNumberFilter: undefined,
        onHouseNumberFilterChange: function () {
            var res = this.houseNumberFilter.split(/\s*-\s*/, 2);
            if (res.length > 0 && res[0]) {
                $scope.rangeData.valueFrom = parseInt(res[0]);
                $scope.rangeData.valueUntil = $scope.rangeData.valueFrom;
            } else {
                $scope.rangeData.valueFrom = $scope.rangeData.min;
                $scope.rangeData.valueUntil = $scope.rangeData.max;
            }
            if (res.length > 1 && res[1]) {
                $scope.rangeData.valueUntil = parseInt(res[1]);
            }
            $scope.onFilterChange();
        }
    };

    /* Range component properties */
    $scope.rangeData = {
        valueFrom: 1,
        valueUntil: 999,
        min: 1,
        max: 999,
        onRangeChange: function () {
            $scope.houseNumberFilterData.houseNumberFilter = this.valueFrom + " - " + this.valueUntil;
            $scope.onFilterChange();
        },
        isFiltred: function () {
            return !(this.valueFrom === this.min && this.valueUntil === this.max);
        },
        reset: function () {
            $scope.rangeData.valueFrom = $scope.rangeData.min;
            $scope.rangeData.valueUntil = $scope.rangeData.max;
        }
    };

    /* Pagination properties */
    $scope.paginationData = {
        maxSize: 5,
        itemsPerPage: 10,
        totalItems: 0,
        currentPage: 1,
        onPageChange: function () {
            getMailAddressList();
        }
    };

    /* Sorting properties */
    $scope.sortData = {
        sortField: 'CreationDate',
        reverse: true,
        sort: function (fieldName) {
            this.reverse = (fieldName !== null && this.sortField === fieldName) ? !this.reverse : false;
            this.sortField = fieldName;
            getMailAddressList();
        },
        isSortUp: function (fieldName) {
            return this.sortField === fieldName && !this.reverse;
        },
        isSortDown: function (fieldName) {
            return this.sortField === fieldName && this.reverse;
        }
    };
};

/* Filter for show rigth format date in filter input */
function dateRangeFilter(dateFilter) {
    return function (input) {
        if (!input.dateFrom) {
            input.dateFrom = input.minDate;
        }
        if (!input.dateUntil) {
            input.dateUntil = new Date();
        }
        return dateFilter(input.dateFrom, 'dd.MM.yyyy') + "-" + dateFilter(input.dateUntil, 'dd.MM.yyyy');
    }
}

/* Localization Service */
function translationService() {
    this.getTranslation = function ($scope, language) {
        var data = null;
        switch (language) {
            case 'en':
                data = {
                    "CAPTION_COUNTRY": "Country",
                    "CAPTION_CITY": "City",
                    "CAPTION_STREET": "Street",
                    "CAPTION_HN": "House #",
                    "CAPTION_INDEX": "Index",
                    "CAPTION_DATE": "Date",
                    "TITLE": "Mail addresses table",
                    "INPUT_RANGE": "Input number range",
                    "RANGE": "Input date range",
                    "PASS": "Records",
                    "FROM": "of total",
                    "PREV": "Previous",
                    "NEXT": "Next"
                };
                break;
            case 'ru':
                data = {
                    "CAPTION_COUNTRY": "Страна",
                    "CAPTION_CITY": "Город",
                    "CAPTION_STREET": "Улица",
                    "CAPTION_HN": "Дом №",
                    "CAPTION_INDEX": "Индекс",
                    "CAPTION_DATE": "Дата",
                    "TITLE": "Таблица почтовых адресов",
                    "INPUT_RANGE": "Введите диапозон номеров",
                    "RANGE": "Укажите период",
                    "PASS": "Обработано",
                    "FROM": "из",
                    "PREV": "Предыдущая",
                    "NEXT": "Следующая"
                };
                break;
        }
        $scope.translation = data;
    };
}
