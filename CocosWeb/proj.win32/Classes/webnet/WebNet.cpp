#include "WebNet.h"

void WebNet::requestData(PBase* _pBase){
	request("requestData", PFactory::instance()->getSendData(_pBase));
}
void WebNet::onHttpRequestCompleted(HttpClient *httpClient, void *data)
{
	HttpResponse* response = (HttpResponse*)data;
	if (!response) { CCLOG("Log:response =null,plase check it."); return; }

	//����ʧ��
	if (!response->isSucceed())
	{
		/*vector<char>* pTemp = response->getResponseData();
		string str(pTemp->begin(), pTemp->end());
		CCLOG("ERROR BUFFER:%s", str);*/
		CCLOG("==============NET ERROR===================");
	}
	else{
		int codeIndex = response->getResponseCode();
		const char* tag = response->getHttpRequest()->getTag();

		//����ɹ�
		vector<char>* buffer = response->getResponseData();
		string temp(buffer->begin(), buffer->end());
		callBack(PFactory::instance()->getBackData(&temp));
		//tinyxml2::XMLElement *toolElement = root->FirstChildElement("test");
	}
}
void WebNet::request(string serviceName, string requestData, bool isUseCallBack){
	HttpRequest* postRequest = new HttpRequest();
	postRequest->setRequestType(HttpRequest::Type::POST);//���÷�������
	postRequest->setUrl((serviceURL + serviceName).c_str());//������ַ
	if (isUseCallBack)
		postRequest->setResponseCallback(CC_CALLBACK_2(WebNet::onHttpRequestCompleted, this));//�ص�������������յ�����Ϣ

	if (requestData != "")
		postRequest->setRequestData(requestData.c_str(), requestData.length());//����Ĵ������������ַ���棬һ���͡�
	postRequest->setTag(serviceName.c_str());
	HttpClient* httpClient = HttpClient::getInstance();
	httpClient->setTimeoutForConnect(10);//�������ӳ�ʱʱ��</span>
	httpClient->setTimeoutForRead(10);//���÷��ͳ�ʱʱ��
	httpClient->send(postRequest);//���ý�����������
	postRequest->release();//�ͷ�
}