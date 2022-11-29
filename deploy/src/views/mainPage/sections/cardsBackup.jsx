/* eslint-disable */
import FileSaver from 'file-saver';
import React, {useCallback} from 'react';
import { Card, Button, CardTitle, CardText, Row, Col, Container } from 'reactstrap';
import "./css/cards.css";

const Cards = () => {
    const exportTxt = useCallback(() => {
        FileSaver.saveAs('/home/ubuntu/','Argandion.zip');
      },[])

      const downloadFile = (url) => {
        url = "/home/ubuntu/"
      
        fetch(url, { method: 'GET' })
            .then((res) => {
                return res.blob();
            })
            .then((blob) => {
                const url = window.URL.createObjectURL(blob);
                const a = document.createElement('a');
                a.href = url;
                a.download = "Argandion.zip";
                document.body.appendChild(a);
                a.click();
                setTimeout((_) => {
                    window.URL.revokeObjectURL(url);
                }, 60000);
                a.remove();
                setOpen(false);
            })
            .catch((err) => {
                console.error('err: ', err);
            });
    };
    const googleButton = () =>{
        alert('서비스 준비 중 입니다. 직접 설치를 이용해 주세요.')
    }

    return (
        <div>
            <div className="spacer" id="card-component">
                <Container>
                    <Row className="justify-content-center">
                        <Col md="7" className="text-center">
                            <h1 className="title font-bold">다운로드</h1>
                            <h6 className="subtitle">아래 두 링크 중 선택하여 다운로드 받으세요. &amp; 다운로드 후 설치하여 플레이 하세요.</h6>
                        </Col>
                    </Row>
                </Container>
            </div>
            
            <Container>
                <Row>
                    <Col md="6">
                        <Card body className="card-shadow">
                            <CardTitle>직접 설치</CardTitle>
                            <CardText>APK파일을 다운로드 받아 직접 설치 합니다.</CardText>
                            {/* <Button onClick={exportTxt}>Go DownLoad</Button> */}
                            <a id="downloadButton" href='./Argandion.zip' Download Now><Button>Go DownLoad</Button></a>
                        </Card>
                    </Col>
                    <Col md="6">
                        <Card body className="card-shadow">
                            <CardTitle>Google Play에서 설치</CardTitle>
                            <CardText>Google Play Store에서 다운로드 및 설치를 합니다.</CardText>
                            <a id="downloadButton"><Button onClick={googleButton}>Go Google Play Store</Button></a>
                            
                        </Card>
                    </Col>
                </Row>
            </Container>
            
        </div>
    );
}

export default Cards;
