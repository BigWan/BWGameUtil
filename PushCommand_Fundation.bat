::����ģ������
SET ToolName=upm-bwgamecode
::����ģ��汾
SET ToolVersion=bwgamecode-fundation-0.0.1
::����ģ��Դ·��
SET ToolAssetPath=Assets/Pck_Core

::������ᴴ��һ��ToolName�ķ�֧����ͬ��ToolAssetPath�µ�����
git subtree split -P %ToolAssetPath% --branch %ToolName%
:: ��ToolName��֧���ñ�ǩToolVersion�ڵ�
git tag %ToolVersion% %ToolName%

:: ���͵�Զ��
git push origin %ToolName% %ToolVersion%
git push origin %ToolName%
pause